using BloodCore.Domain;
using System.Collections.Generic;

namespace BloodLoop.Domain.Donations
{
    public class BloodType : ValueObject
    {
        public string Label { get; private set; }
        public string Symbol { get; private set; }

        #region Constructors

        private BloodType() { }

        internal BloodType(string label, string symbol)
        {
            Label = label;
            Symbol = symbol;
        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
            yield return Symbol;
        }

        #region Defaults

        public static BloodType Zero_Rh_Plus => new BloodType(nameof(Zero_Rh_Plus).ToLower(), "0-");
        public static BloodType Zero_Rh_Minus => new BloodType(nameof(Zero_Rh_Minus).ToLower(), "0+");
        public static BloodType A_Rh_Plus => new BloodType(nameof(A_Rh_Plus).ToLower(), "A-");
        public static BloodType A_Rh_Minus => new BloodType(nameof(A_Rh_Minus).ToLower(), "A+");
        public static BloodType B_Rh_Plus => new BloodType(nameof(B_Rh_Plus).ToLower(), "B-");
        public static BloodType B_Rh_Minus => new BloodType(nameof(B_Rh_Minus).ToLower(), "B+");
        public static BloodType AB_Rh_Plus => new BloodType(nameof(AB_Rh_Plus).ToLower(), "AB-");
        public static BloodType AB_Rh_Minus => new BloodType(nameof(AB_Rh_Minus).ToLower(), "AB+");

        public static BloodType[] GetBloodTypes()
        {
            return new[]
            {
                Zero_Rh_Plus, Zero_Rh_Minus,
                A_Rh_Plus, A_Rh_Minus,
                B_Rh_Plus, B_Rh_Minus,
                AB_Rh_Plus, AB_Rh_Minus,
            };
        }

        #endregion
    }
}