using System;
using System.ComponentModel.DataAnnotations;

namespace Danstagram.Feed.Service.Dtos
{
    public record ItemDto(Guid Id, byte[] Image, string Caption, int LikeCount, DateTimeOffset CreatedDate);

    public record CreateItemDto([Required] byte[] Image, string Caption);

    public record UpdateItemDto(string Caption,[Range(0,1000)] int LikeCount);
} 