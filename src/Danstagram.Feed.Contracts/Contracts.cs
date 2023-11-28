using System;

namespace Danstagram.Feed.Contracts
{
    #region Properties

    public record FeedItemCreated(Guid Id);

    public record FeedItemDeleted(Guid Id);

    #endregion
}