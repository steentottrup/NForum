using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core.Providers {

	public class SynchronousOutData : IOutDataService {
		protected readonly IMailService mailService;

		public SynchronousOutData(IMailService mailService) {
			this.mailService = mailService;
		}

		public Boolean Send(String subject, String textBody, String recipientEmailAddress, String recipientName, String senderEmailAddress, String senderName, MailType type) {
			return this.mailService.Send(subject, textBody, recipientEmailAddress, recipientName, senderEmailAddress, senderName);
		}
	}
}