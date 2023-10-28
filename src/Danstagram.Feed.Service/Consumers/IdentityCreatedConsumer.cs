using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Service.Entities;
using Danstagram.Identities.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers{
    public class IdentityCreatedConsumer : RepositoryConsumer<IdentityItem>,IConsumer<IdentityCreated>
    {
        #region Constructors
        public IdentityCreatedConsumer(IRepository<IdentityItem> repository) : base(repository)
        {
        }
        #endregion


        #region Methods
        public async Task Consume(ConsumeContext<IdentityCreated> context)
        {
            var message = context.Message;
            if ((await repository.GetAsync(message.Id)) != null) return;

            IdentityItem identity = new() {Id = message.Id,UserName = message.UserName};

            await this.repository.CreateAsync(identity);
        }
        #endregion
    }
}