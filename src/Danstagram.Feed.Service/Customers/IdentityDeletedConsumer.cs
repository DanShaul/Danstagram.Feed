using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Service.Entities;
using Danstagram.Identities.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers{
    public class IdentityDeletedConsumer : RepositoryConsumer<IdentityItem>,IConsumer<IdentityDeleted>
    {
        #region Constructors
        public IdentityDeletedConsumer(IRepository<IdentityItem> repository) : base(repository)
        {
        }
        #endregion


        #region Methods
        public async Task Consume(ConsumeContext<IdentityDeleted> context)
        {
            var message = context.Message;
            if ((await repository.GetAsync(message.Id)) == null) return;
            
            await this.repository.RemoveAsync(message.Id);
        }
        #endregion
    }
}