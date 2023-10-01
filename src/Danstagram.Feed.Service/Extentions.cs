using Danstagram.Feed.Service.Dtos;
using Danstagram.Feed.Service.Entities;

namespace Danstagram.Feed.Service{
    public static class Extentions{
        public static ItemDto AsDto(this FeedItem item) {
            return new ItemDto(item.Id, item.Image, item.Caption,item.LikeCount,item.CreatedDate);
        }
    }
}