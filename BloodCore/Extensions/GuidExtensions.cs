using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Extensions
{
    public static class IdentityExtensions
    {
        public static string ToShort(this Identity identity)
        {
            return identity.Id.ToShort();
        }
    }

    public static class GuidExtensions
    {
        public static Guid ParseShort(string guidBase64)
        {
            var guids = DesanitizeBase64(guidBase64) + "==";

            var guid = new Guid(Convert.FromBase64String(guids));

            return guid;
        }

        public static bool TryParseShort(string guidBase64, out Guid guid)
        {
            try
            {
                guid = ParseShort(guidBase64);
            }
            catch
            {
                guid = Guid.Empty;
                return false;
            }
            return true;
        }

        public static string ToShort(this Guid guid)
        {
            var result = Convert.ToBase64String(guid.ToByteArray());

            return SanitizeBase64(result).Replace("==", "");
        }

        private static string SanitizeBase64(string value)
        {
            return value.Replace("+", "-").Replace("/", "_");
        }

        private static string DesanitizeBase64(string value)
        {
            return value.Replace("-", "+").Replace("_", "/");
        }
    }
}
