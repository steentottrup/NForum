using NForum.Core.Abstractions.Services;
using System;
using System.Net.Mail;

namespace NForum.Core.Services {

	public class SMTPMailService : IMailService {

		public Boolean Send(String subject, String textBody, String recipientEmailAddress, String recipientName, String senderEmailAddress, String senderName) {
			MailMessage message = new MailMessage();
			message.Subject = subject;
			message.Body = textBody;
			message.IsBodyHtml = false;

			message.To.Add(new MailAddress(recipientEmailAddress, recipientName));
			message.From = new MailAddress(senderEmailAddress, senderName);

			using (SmtpClient client = new SmtpClient()) {
				client.Send(message);
			}

			return true;
		}
	}
}