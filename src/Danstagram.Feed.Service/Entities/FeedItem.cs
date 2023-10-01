using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities
{
    public class FeedItem : IEntity {
        public Guid Id { get; set; }
        public Guid UserId{get;set;}

        public byte[] Image { get; set; }

        public string Caption { get; set; }

        public int LikeCount { get; set; }

        public DateTimeOffset CreatedDate{ get; set; }
    }
}