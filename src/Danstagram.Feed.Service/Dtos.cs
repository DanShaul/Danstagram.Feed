using System;
using System.ComponentModel.DataAnnotations;

namespace Danstagram.Feed.Service.Dtos
{
    #region Properties
    public record ItemDto(Guid Id,
                        String UserName,
                        byte[] Image,
                        int LikeCount,
                        string Caption,
                        DateTimeOffset CreatedDate);
    public record CreateItemDto(Guid UserId,
                                byte[] Image,
                                string Caption);
    public record DeleteItetmDto(Guid ImageId);
    #endregion

} 