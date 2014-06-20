using System;

namespace NForum.Core.Abstractions.Services {

	public interface IMailService {
		Boolean Send(String subject,
						String textBody,
						String recipientEmailAddress,
						String recipientName,
						String senderEmailAddress,
						String senderName);
	}
}