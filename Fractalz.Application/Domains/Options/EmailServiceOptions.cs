namespace  Fractalz.Application.Domains.Options
{
    public class EmailServiceOptions
    {
        public const string EmailService = "EmailServiceOptions";
        public string FromAddress { get; set; }
        public string Name { get; set; }
        public string HostAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}