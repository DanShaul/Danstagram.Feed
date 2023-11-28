using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Service.Entities;
using Danstagram.Interactions.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers
{
    public class LikeDeletedConsumer : IConsumer<LikeDeleted>
    {
        #region Constructors
        public LikeDeletedConsumer(IRepository<LikeItem> repository)
        {
            this.repository = repository;
        }


        #endregion

        #region Properties
        private readonly IRepository<LikeItem> repository;
        #endregion

        #region Methods
        public async Task Consume(ConsumeContext<LikeDeleted> context)
        {
            LikeDeleted message = context.Message;
            if (await repository.GetAsync(message.Id) == null)
            {
                return;
            }

            await repository.RemoveAsync(message.Id);
        }
        #endregion
    }
}