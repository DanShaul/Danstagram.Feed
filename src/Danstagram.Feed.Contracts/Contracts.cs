using System;

namespace Danstagram.Feed.Contracts{
    public record FeedItemCreated(Guid ItemId,byte[] Image,string Caption,int LikeCount);

    public record FeedItemUpdated(Guid ItemId,byte[] Image,string Caption,int LikeCount);

    public record FeedItemDeleted(Guid ItemId);
}