using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Service.Entities;
using Danstagram.Interactions.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers
{
    public class LikeCreatedConsumer : IConsumer<LikeCreated>
    {
        #region Constructors
        public LikeCreatedConsumer(IRepository<LikeItem> repository)
        {
            this.repository = repository;
        }


        #endregion

        #region Properties
        private readonly IRepository<LikeItem> repository;
        #endregion

        #region Methods
        public async Task Consume(ConsumeContext<LikeCreated> context)
        {
            LikeCreated message = context.Message;
            if (await repository.GetAsync(message.Id) != null)
            {
                return;
            }

            LikeItem likeItem = new()
            {
                Id = message.Id,
                UserId = message.UserId,
                FeedItemId = message.FeedItemId
            };

            await repository.CreateAsync(likeItem);
        }
        #endregion
    }
}