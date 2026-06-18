using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Utility
{
	public class Email : IEmail
	{
		private readonly IConfiguration _config;
		public Email(IConfiguration config)
		{
			_config = config;
		}

		public void SendEmail(EmailModel emailModel)
		{
			var emailmessage = new MimeMessage();
			string from = _config.GetSection("EmailSettings:From").Value;
			emailmessage.From.Add(new MailboxAddress("Leave Tracking", from));
			emailmessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
			emailmessage.Subject = emailModel.Subject;
			emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = string.Format(emailModel.Content)
			};
			using(var client = new SmtpClient())
			{
				try
				{
					client.Connect(_config["EmailSettings:SmtpServer"], 465, true);
					client.Authenticate(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
					client.Send(emailmessage);
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
				
			}
		}
	}
}
