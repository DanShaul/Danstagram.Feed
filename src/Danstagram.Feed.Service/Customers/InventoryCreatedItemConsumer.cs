using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Contracts;
using Danstagram.Feed.Service.Entities;
using Danstagram.Inventory.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers{
    public class InventoryItemCreatedConsumer : IConsumer<InventoryItemCreated>
    {
        private readonly IRepository<FeedItem> repository;
        public InventoryItemCreatedConsumer(IRepository<FeedItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<InventoryItemCreated> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ItemId);

            if(item != null){
                return;
            }

            item = new FeedItem{
                Id = message.ItemId,
                UserId = message.UserId,
                Image = message.Image,
                Caption = message.Caption,
                LikeCount = message.LikeCount,
                CreatedDate = message.CreatedDate
            };

            await repository.CreateAsync(item);
        }
    }
}