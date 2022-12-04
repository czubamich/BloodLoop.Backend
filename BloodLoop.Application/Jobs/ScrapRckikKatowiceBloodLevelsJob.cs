using BloodCore;
using BloodCore.Persistance;
using BloodCore.Services;
using BloodCore.Workers;
using BloodLoop.Application.Providers.RckikKatowice;
using BloodLoop.Application.Specifications;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using Serilog;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BloodLoop.Application.Jobs
{
    [Injectable]
    public class ScrapRckikKatowiceBloodLevelsJob : IRecurringJob
    {
        public string Name => nameof(ScrapRckikKatowiceBloodLevelsJob);

        const string RCKIK_KATOWICE_LABEL = "rckik-ktw";

        private readonly IRckikKatowiceClient _client;
        private readonly IBloodBankRepository _repository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IUnitOfWork _unitOfWork;

        public ScrapRckikKatowiceBloodLevelsJob(IUnitOfWork unitOfWork, IRckikKatowiceClient client, IBloodBankRepository repository, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _client = client;
            _repository = repository;
            _dateTimeService = dateTimeService;
        }

        public async Task ScrapBloodLevels()
        {
            DateTime measurementTime = _dateTimeService.Now();

            var bloodLevels = await _client.GetBloodLevels();

            if (!bloodLevels.Any() || bloodLevels.First().Date.Date < measurementTime.Date)
                throw new InvalidOperationException("Rckik Data is not fresh yet!");

            var rckik = await _repository.Get(new BloodBankByLabelSpec(RCKIK_KATOWICE_LABEL));

            var previousMeasurement = rckik.BloodLevels.OrderByDescending(x => x.CreatedAt).FirstOrDefault()?.CreatedAt;
            if (previousMeasurement.HasValue && previousMeasurement >= bloodLevels.First().Date)
            {
                Log.Information("BloodLevels scrapping already done for today: {Date}", measurementTime.Date);
                return;
            }

            await _unitOfWork.BeginTransactionAsync();

            bloodLevels
                .ForEach(bl => 
                {
                    try
                    {
                        var bloodType = MapToBloodType(bl.Slug);
                        var value = int.Parse(bl.Acf.Level);

                        rckik.AddBloodLevel(bloodType, value, measurementTime);

                        Log.Information("[{Source}][{BloodType}] New BloodLevel: {BloodLevel} - {MeasurementDate}", rckik.Name, bloodType.Symbol, value, measurementTime.ToString("dd.MM.yyyy"));
                    }
                    catch(Exception e)
                    {
                        Log.Error(e, "Adding BloodLevel {Slug}, {Level} failed", bl.Slug, bl.Acf.Level);
                    }
                });

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public BloodType MapToBloodType(string bloodType)
        {
            return bloodType switch
            {
                "0-rh" => BloodType.Zero_Rh_Minus,
                "0-rh-2" => BloodType.Zero_Rh_Plus,
                "a-2" => BloodType.A_Rh_Minus,
                "a" => BloodType.A_Rh_Plus,
                "b-2" => BloodType.B_Rh_Minus,
                "b" => BloodType.B_Rh_Plus,
                "ab-2" => BloodType.AB_Rh_Minus,
                "ab" => BloodType.AB_Rh_Plus,
                _ => throw new ArgumentOutOfRangeException("Invalid bloodType slug")
            };
        }

        public void Configure(IJobScheduler scheduler, RecurringJobDefinition settings)
        {
            scheduler.AddOrReplaceRecurring<ScrapRckikKatowiceBloodLevelsJob>(settings.JobName, j => j.ScrapBloodLevels(), settings.Cron);
        }
    }
}
