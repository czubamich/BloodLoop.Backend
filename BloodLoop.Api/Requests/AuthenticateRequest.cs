namespace BloodLoop.Api.Requests
{
    public class AuthenticateRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}