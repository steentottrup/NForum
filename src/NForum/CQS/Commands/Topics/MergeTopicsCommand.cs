using CreativeMinds.CQS.Commands;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class MergeTopicsCommand : ICommand {
		/// <summary>
		/// The destination topic, the topic the other topics are merged into.
		/// The subject and content of this topic will be the subject and content of the merged topic.
		/// </summary>
		public String DestinationTopicId { get; set; }
		/// <summary>
		/// THe ids of the topics that will be merged into the destination topic.
		/// When all replies from these topics have been moved to the destionation topic, these topics will be deleted.
		/// </summary>
		public String[] SourceTopicIds { get; set; }
		/// <summary>
		/// If this property is set to true, replies will be created with the subject and content of the destionation topics.
		/// </summary>
		public Boolean CreateReplyForTopics { get; set; }
	}
}
