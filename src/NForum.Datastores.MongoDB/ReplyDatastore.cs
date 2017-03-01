using System;
using NForum.Core.Dtos;
using MongoDB.Bson;
using NForum.Datastores.MongoDB.Dbos;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Datastores.MongoDB {

	public class ReplyDatastore : IReplyDatastore {
		protected readonly CommonDatastore datastore;

		public ReplyDatastore(CommonDatastore datastore) {
			this.datastore = datastore;
		}

		public IReplyDto Create(Domain.Reply reply) {
			Dbos.Topic topic = this.datastore.ReadTopicById(ObjectId.Parse(reply.TopicId));
			if (topic == null) {
				// TODO ??
				throw new ArgumentException("Parent topic not found");
			}

			Dbos.Reply r = new Dbos.Reply {
				Content = reply.Content,
				Created = reply.Created,
				//CreatedBy = 
				LastEdited = reply.LastEdited,
				//LastEditedBy = 
				State = reply.State,
				Subject = reply.Subject,
				Topic = new TopicRef {
					Id = ObjectId.Parse(reply.Topic.Id),
					Subject = reply.Topic.Subject
				}
			};

			if (!String.IsNullOrWhiteSpace(reply.ReplyToId)) {
				Dbos.Reply parentReply = this.datastore.ReadReplyById(ObjectId.Parse(reply.ReplyToId));
				if (parentReply == null) {
					// TODO ??
					throw new ArgumentException("Parent reply not found");
				}

				r.ReplyTo = new Dbos.ReplyRef {
					Id = parentReply.Id,
					Name = parentReply.Subject
				};
			}

			return this.datastore.CreateReply(r).ToDto();
		}

		public void MergeTopics(String destinationTopicId, IEnumerable<String> sourceTopicIds) {
			ObjectId[] sourceIds = sourceTopicIds.Select(i => ObjectId.Parse(i)).ToArray();
			ObjectId destinationId = ObjectId.Parse(destinationTopicId);

			//this.datastore.
			throw new NotImplementedException();
		}

		public IReplyDto ReadById(String id) {
			ObjectId replyId;
			if (!ObjectId.TryParse(id, out replyId)) {
				throw new ArgumentException(nameof(id));
			}
			return this.datastore.ReadReplyById(replyId).ToDto();
		}
	}
}
