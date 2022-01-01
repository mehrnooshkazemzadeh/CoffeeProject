using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Tools
{
    public class MailService
    {

        private string _host;
        private string _userName;
        private string _password;
        private int _port;
        private bool _isSslRequired;
        private string _mailStatus;
        private readonly ILogger<MailService> logger;

        public string Host
        {
            get => _host;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _host = value;
                }

            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _userName = value;
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _password = value;
                }
            }
        }

        public int Port
        {
            get => _port;
            set => _port = value;
        }

        public bool IsSslRequired
        {
            get => _isSslRequired;
            set => _isSslRequired = value;
        }

        public MailService()
        {

        }
        public MailService(string host, string username, string password, ILogger<MailService> logger, int port = 25, bool ssl = true)
        {
            Host = host;
            UserName = username;
            Password = password;
            Port = port;
            IsSslRequired = ssl;
            this.logger = logger;
        }

        private bool IsSmtpParametersValid
        {
            get
            {
                var hasHost = string.IsNullOrWhiteSpace(_host);
                var hasUserName = string.IsNullOrWhiteSpace(_userName);
                var hasPassword = string.IsNullOrWhiteSpace(_password);
                return !(hasHost || hasUserName || hasPassword);
            }
        }

        private string Message
        {
            get
            {
                var hostMessage = string.IsNullOrWhiteSpace(_host)
                    ? "Host is Empty" + Environment.NewLine
                    : string.Empty;
                var userNameMessage = string.IsNullOrWhiteSpace(_userName)
                   ? "Username is Empty" + Environment.NewLine
                   : string.Empty;
                var passwordMessage = string.IsNullOrWhiteSpace(_password)
                   ? "Password is Empty" + Environment.NewLine
                   : string.Empty;
                return hostMessage + userNameMessage + passwordMessage;
            }
        }

        public string GetSendAsyncStatus
        {
            get
            {
                if (!string.IsNullOrEmpty(_mailStatus))
                {
                    return _mailStatus;
                }
                return "NotSent";
            }
        }


        public void Send(string subject, string fromMailAddress, string displayName, string body, bool isMailBodyHtml, MailPriority mailPriority, List<string> toMailAddresses, List<string> ccMailAddresses, List<string> bccMailAddresses, Dictionary<string, byte[]> attachments)
        {
            try
            {
                SmtpClient smtp;
                var mailMessage = CreateMail(subject, fromMailAddress, displayName, body, isMailBodyHtml, mailPriority,
                    toMailAddresses, ccMailAddresses, bccMailAddresses, attachments, out smtp);

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                var message = ExceptionParser.Parse(ex);
 if (logger != null)
                {
                    logger.LogError(message, ex);
                }

            }


        }
        public async Task SendMailAsync(string to, string subject, string fromMailAddress, string displayName, string body)
        {
            await SendMailAsync(subject, fromMailAddress, displayName, body, toMailAddresses: new List<string> { to });
        }
        public async Task SendMailAsync(string to, string subject, string fromMailAddress, string displayName, string body, Dictionary<string, byte[]> attachments = null)
        {
            await SendMailAsync(subject, fromMailAddress, displayName, body, toMailAddresses: new List<string> { to }, attachments: attachments);
        }

        public async Task SendMailAsync(string subject, string fromMailAddress, string displayName, string body,
            bool isMailBodyHtml = true, MailPriority mailPriority = MailPriority.High, List<string> toMailAddresses = null,
            List<string> ccMailAddresses = null, List<string> bccMailAddresses = null, Dictionary<string, byte[]> attachments = null)
        {
            try
            {
                _mailStatus = string.Empty;
                SmtpClient smtp;
                var mailMessage = CreateMail(subject, fromMailAddress, displayName, body, isMailBodyHtml, mailPriority, toMailAddresses, ccMailAddresses, bccMailAddresses, attachments, out smtp);
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                var message = ExceptionParser.Parse(ex);
   if (logger !=null)
                {
                    logger.LogError(message, ex);
                }
            }

        }

        public void SendAsync(string subject, string fromMailAddress, string displayName, string body, bool isMailBodyHtml, MailPriority mailPriority, List<string> toMailAddresses, List<string> ccMailAddresses, List<string> bccMailAddresses, Dictionary<string, byte[]> attachments)
        {
            try
            {
                _mailStatus = string.Empty;
                SmtpClient smtp;
                var mailMessage = CreateMail(subject, fromMailAddress, displayName, body, isMailBodyHtml, mailPriority, toMailAddresses, ccMailAddresses, bccMailAddresses, attachments, out smtp);
                var token = "NotSent";
                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallBack);
                smtp.SendAsync(mailMessage, token);
            }
            catch (Exception ex)
            {
                var message = ExceptionParser.Parse(ex);
                if (logger != null)
                {
                    logger.LogError(message, ex);
                }
            }

        }

        private void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)
        {
            string token = (string)e.UserState;
            if (e.Cancelled)
            {
                _mailStatus = "Cancel";
            }
            if (e.Error != null)
            {
                _mailStatus = token + ":" + e.Error;
            }
            else
            {
                _mailStatus = "Sent";
            }
        }

        private MailMessage CreateMail(string subject,
                                              string fromMailAddress,
                                              string displayName,
                                              string body,
                                              bool isMailBodyHtml,
                                              MailPriority mailPriority,
                                              List<string> toMailAddresses,
                                              List<string> ccMailAddresses,
                                              List<string> bccMailAddresses,
                                              Dictionary<string, byte[]> attachments,
                                              out SmtpClient smtp)
        {
            if (!IsSmtpParametersValid)
            {
                throw new Exception(Message);
            }
            var mailMessage = new MailMessage
            {
                Subject = subject,
                From = new MailAddress(fromMailAddress, displayName),
                IsBodyHtml = isMailBodyHtml,
                Priority = mailPriority,
                Body = body,
                BodyEncoding = Encoding.UTF8,
            };
            if (toMailAddresses != null)
            {
                toMailAddresses.ForEach(x => mailMessage.To.Add(x));
            }

            if (ccMailAddresses != null)
            {
                ccMailAddresses.ForEach(x => mailMessage.To.Add(x));
            }
            if (bccMailAddresses != null)
            {
                bccMailAddresses.ForEach(x => mailMessage.To.Add(x));
            }

            if (attachments != null)
            {

                foreach (var attach in attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(attach.Value), attach.Key));
                }
            }
            smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = _isSslRequired,
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential(_userName, _password)
            };
            return mailMessage;
        }
    }
}
