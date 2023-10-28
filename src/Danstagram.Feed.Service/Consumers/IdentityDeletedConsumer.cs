using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Contracts;
using Danstagram.Feed.Service.Entities;
using Danstagram.Identities.Contracts;
using MassTransit;

namespace Danstagram.Feed.Service.Consumers
{
  public class IdentityDeletedConsumer : RepositoryConsumer<IdentityItem>, IConsumer<IdentityDeleted>
  {
    #region Constructors
    public IdentityDeletedConsumer(IRepository<IdentityItem> repository, IRepository<FeedItem> feedRepository, IPublishEndpoint publishEndpoint) : base(repository)
    {
      this.feedRepository = feedRepository;
      this.publishEndpoint = publishEndpoint;

    }
    #endregion
    #region Properties
    private readonly IRepository<FeedItem> feedRepository;
    private readonly IPublishEndpoint publishEndpoint;
    #endregion


    #region Methods
    public async Task Consume(ConsumeContext<IdentityDeleted> context)
    {
      IdentityDeleted message = context.Message;
      if ((await repository.GetAsync(message.Id)) == null)
      {
        return;
      }

      await repository.RemoveAsync(message.Id);

      System.Collections.Generic.IReadOnlyCollection<FeedItem> feedItems = await feedRepository.GetAllAsync(item => item.UserId == message.Id);

      foreach (FeedItem feedItem in feedItems)
      {
        await feedRepository.RemoveAsync(feedItem.Id);
        await publishEndpoint.Publish(new FeedItemDeleted(feedItem.Id));
      }

    }
    #endregion
  }
}