using System;
using System.ComponentModel.DataAnnotations;

namespace Danstagram.Feed.Service.Dtos
{
    #region Properties
    public record FeedItemDto(Guid Id,
                        String UserName,
                        byte[] Image,
                        int LikeCount,
                        string Caption,
                        DateTimeOffset CreatedDate);
    public record CreateFeedItemDto(Guid UserId,
                                byte[] Image,
                                string Caption);
    public record DeleteFeedItetmDto(Guid ImageId);
    #endregion

} 