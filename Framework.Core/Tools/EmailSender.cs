using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Framework.Core.Tools;
using System.Threading.Tasks;

namespace Framework.Core.Tools
{
    public class EmailSender : MailService, IEmailSender
    {
        private readonly string from;

        public EmailSender()
        {

        }
        public EmailSender(IConfiguration configuration, string configName)
        {
            var identityConfig = configuration.GetSection("MailServer").GetSection(configName);
            Host = identityConfig.GetValue<string>("host");
            Port = identityConfig.GetValue<int>("port");
            UserName = identityConfig.GetValue<string>("username");
            Password = identityConfig.GetValue<string>("password");
            IsSslRequired = identityConfig.GetValue<bool>("ssl");
            from = identityConfig.GetValue<string>("from");
        }
        public EmailSender(string from,string host, string username, string password, int port, bool ssl, ILogger<MailService> logger) : base(host, username, password, logger, port, ssl)
        {
            this.from = from;
            Host = host;
            Port = port;
            UserName = username;
            Password = password;
            IsSslRequired = ssl;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMailAsync(email, subject, from, from, htmlMessage);
        }

   
    }
}
