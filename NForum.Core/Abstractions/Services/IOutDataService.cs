using System;
using System.Net.Mail;

namespace NForum.Core.Abstractions.Services {

	public interface IOutDataService {
		Boolean Send(String subject,
						String textBody,
						String recipientEmailAddress,
						String recipientName,
						String senderEmailAddress,
						String senderName,
						MailType type);
	}
}