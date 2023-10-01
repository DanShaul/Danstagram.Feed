using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Contracts;
using Danstagram.Feed.Service.Entities;
using Danstagram.Inventory.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers{
    public class InventoryItemDeletedConsumer : IConsumer<InventoryItemDeleted>
    {
        private readonly IRepository<FeedItem> repository;
        public InventoryItemDeletedConsumer(IRepository<FeedItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<InventoryItemDeleted> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ItemId);

            if(item == null)
            {
                return;
            }
            else
            {
                await repository.RemoveAsync(item.Id);
            }
        }
    }
}