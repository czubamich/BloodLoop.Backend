namespace BloodLoop.Application.Auth.Requests
{
    public class AuthenticateRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}