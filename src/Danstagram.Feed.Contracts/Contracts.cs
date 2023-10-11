using System;

namespace Danstagram.Feed.Contracts{
    #region Properties

    public record FeedItemCreated(Guid Id);

    //public record FeedItemUpdated(Guid ItemId,byte[] Image,string Caption,int LikeCount);

    public record FeedItemDeleted(Guid Id);

    #endregion
}