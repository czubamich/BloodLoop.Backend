using System;

namespace BloodCore.AspNet
{
    public class JwtToken
    {
        public string Token { get; }
        public DateTime ExpireAt { get; }

        public JwtToken(string token, int expirationTimeMinutes)
        {
            Token = token ?? throw new ArgumentNullException(nameof(Token));
            ExpireAt = DateTime.Now.AddMinutes(expirationTimeMinutes);
        }
    }
}
