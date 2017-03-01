using CreativeMinds.CQS.Commands;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class MoveTopicCommand : ICommand {
		/// <summary>
		/// The id of the topic that will be moved from one forum to another.
		/// </summary>
		public String TopicId { get; set; }
		/// <summary>
		/// The id of the destionation forum, the new parent for the topic.
		/// </summary>
		public String DestinationForumId { get; set; }
		/// <summary>
		/// If this property is set to true, the original topic will not be moved, but changed into a 'topic was moved' type topic,
		/// and a new topic will be created in the destionation forum. This topic is identical to the original topic, but with a diffent íd.
		/// </summary>
		public Boolean CreateMovedTopic { get; set; }
	}
}
