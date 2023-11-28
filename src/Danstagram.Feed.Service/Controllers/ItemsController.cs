using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Contracts;
using Danstagram.Feed.Service.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Danstagram.Feed.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        #region Properties

        private readonly IRepository<FeedItem> feedItemsRepository;
        private readonly IRepository<IdentityItem> identityItemsRepository;
        private readonly IRepository<LikeItem> likeItemsRepository;
        private readonly IPublishEndpoint publishEndpoint;

        #endregion

        #region Constructors

        public ItemsController(IRepository<FeedItem> feedItemsRepository,
                                IRepository<IdentityItem> identityItemsRepository,
                                IRepository<LikeItem> likeItemsRepository,
                                IPublishEndpoint publishEndpoint)
        {
            this.feedItemsRepository = feedItemsRepository;
            this.identityItemsRepository = identityItemsRepository;
            this.likeItemsRepository = likeItemsRepository;

            this.publishEndpoint = publishEndpoint;
        }

        #endregion

        #region Methods
        [HttpGet]
        public async Task<IEnumerable<FeedItemDto>> GetItemsAsync()
        {
            IReadOnlyCollection<FeedItem> feedItems = await feedItemsRepository.GetAllAsync();

            IEnumerable<Guid> feedItemIds = feedItems.Select(item => item.Id);
            IReadOnlyCollection<LikeItem> likeItems = await likeItemsRepository.GetAllAsync(item => feedItemIds.Contains(item.FeedItemId));

            IEnumerable<Guid> feedUserIds = feedItems.Select(item => item.UserId);
            IReadOnlyCollection<IdentityItem> identityItems = await identityItemsRepository.GetAllAsync(item => feedUserIds.Contains(item.Id));



            IEnumerable<FeedItemDto> items = feedItems.Select(item => new FeedItemDto(
                      item.Id,
                      identityItems.SingleOrDefault(identity => identity.Id == item.UserId).UserName,
                      item.Image,
                      likeItems.Select(likeItem => likeItem.FeedItemId == item.Id).Count(),
                      item.Caption,
                      item.CreatedDate));

            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedItemDto>> GetByIdAsync(Guid id)
        {
            FeedItem item = await feedItemsRepository.GetAsync(id);
            if (item == null)
            {
                return (ActionResult<FeedItemDto>)NotFound();
            }
            IReadOnlyCollection<LikeItem> itemLikes = await likeItemsRepository.GetAllAsync(item => item.FeedItemId == id);
            IdentityItem itemUserIdentity = await identityItemsRepository.GetAsync(item.UserId);
            FeedItemDto itemDto = new(
                item.Id,
                itemUserIdentity.UserName,
                item.Image,
                itemLikes.Count,
                item.Caption,
                item.CreatedDate);

            return (ActionResult<FeedItemDto>)itemDto;
        }

        // Post /items
        [HttpPost]
        public async Task<ActionResult<CreateFeedItemDto>> PostAsync(CreateFeedItemDto createItemDto)
        {
            if ((await identityItemsRepository.GetAsync(createItemDto.UserId)) == null)
            {
                return (ActionResult<CreateFeedItemDto>)NotFound("User not found in database");
            }
            FeedItem item = new()
            {
                Id = Guid.NewGuid(),
                UserId = createItemDto.UserId,
                Image = createItemDto.Image,
                Caption = createItemDto.Caption,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await feedItemsRepository.CreateAsync(item);

            await publishEndpoint.Publish(new FeedItemCreated(item.Id));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }


        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            FeedItem item = await feedItemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            await feedItemsRepository.RemoveAsync(item.Id);

            await publishEndpoint.Publish(new FeedItemDeleted(item.Id));

            return NoContent();
        }
        #endregion
    }
}