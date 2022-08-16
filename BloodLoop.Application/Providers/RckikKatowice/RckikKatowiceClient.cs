using BloodCore;
using BloodLoop.Domain.BloodBanks;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodLoop.Application.Providers.RckikKatowice
{
    [Injectable]
    public class RckikKatowiceClient : IRckikKatowiceClient
    {
        public string Url => "https://rckik-katowice.pl/";

        public async Task<List<RK_BloodRS>> GetBloodLevels()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url+"wp/wp-json/wp/v2/krew");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            
            return await JsonSerializer.DeserializeAsync<List<RK_BloodRS>>(stream);    
        }
    }

    public class RK_BloodRS
    {
        [JsonPropertyName("modified")]
        public DateTime Date { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("acf")]
        public RK_AcfRS Acf { get; set; }
    }

    public class RK_AcfRS
    {
        [JsonPropertyName("level")]
        public string Level { get; set; }
    }
}
