using System;

namespace Danstagram.Feed.Service
{
    #region Properties
    public record FeedItemDto(Guid Id,
                        string UserName,
                        byte[] Image,
                        int LikeCount,
                        string Caption,
                        DateTimeOffset CreatedDate);
    public record CreateFeedItemDto(Guid UserId,
                                byte[] Image,
                                string Caption);
    public record DeleteFeedItemDto(Guid ImageId);
    #endregion

}