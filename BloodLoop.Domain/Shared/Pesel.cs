using Ardalis.GuardClauses;
using BloodCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BloodLoop.Domain.Donors
{
    public class Pesel : ValueObject
    {
        public string Value { get; private set; }

        public Pesel(string pesel)
        {
            Value = Guard.Against.InvalidFormat(pesel, nameof(Pesel), "^[0-9]{11}$", "Invalid Pesel Format");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Pesel Parse(string pesel) => new Pesel(pesel);

        public static bool TryParse(string pesel, out Pesel result)
        {
            try
            {
                result = Parse(pesel);
                return true;
            }
            catch(System.ArgumentException)
            {
                result = null;
                return false;
            }
        }

        public override string ToString() => Value;

        public bool IsValid()
        {
            if (Value.Length != 11)
                return false;

            int[] controlWeights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int value = Value
                .Take(10)
                .Select(x => int.Parse(x.ToString()))
                .Zip(controlWeights)
                .Select((x) => (x.Item1 * x.Item2) % 10)
                .Sum();

            int controlNumber = 10 - (value % 10);

            return controlNumber == int.Parse(Value.Last().ToString());
        }
    }
}