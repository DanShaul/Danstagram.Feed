using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Danstagram.Common;
using Danstagram.Feed.Service.Dtos;
using Danstagram.Feed.Service.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Danstagram.Feed.Contracts;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using MassTransit.Internals;
using Microsoft.AspNetCore.Http.Features;
using System.Net.NetworkInformation;

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

            var feedItems = await feedItemsRepository.GetAllAsync();

            var feedItemIds = feedItems.Select(item => item.Id);
            var likeItems = await likeItemsRepository.GetAllAsync(item => feedItemIds.Contains(item.FeedItemId));

            var feedUserIds = feedItems.Select(item => item.UserId);
            var identityItems = await identityItemsRepository.GetAllAsync(item => feedUserIds.Contains(item.Id));

            var items = feedItems.Select(item => new FeedItemDto(
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
            if(item == null){
                return (ActionResult<FeedItemDto>)NotFound();
            }
            var itemLikes = await likeItemsRepository.GetAllAsync(item => item.FeedItemId == id);
            var itemUserIdentity = await identityItemsRepository.GetAsync(item.UserId);
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
            if (identityItemsRepository.GetAsync(createItemDto.UserId) == null){
                return (ActionResult<CreateFeedItemDto>)NotFound();
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

            // await publishEndpoint.Publish(new FeedItemCreated(item.Id, item.Image, item.Caption, item.LikeCount));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        // // PUT /items/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        // {
        //     FeedItem existingItem = await itemsRepository.GetAsync(id);
        //     if (existingItem == null)
        //     {
        //         return NotFound();
        //     }
        //     if (updateItemDto.Caption != null)
        //     {
        //         existingItem.Caption = updateItemDto.Caption;
        //     }
        //     if (updateItemDto.LikeCount != 0)
        //     {
        //         existingItem.LikeCount = updateItemDto.LikeCount;
        //     }

        //     await itemsRepository.UpdateAsync(existingItem);

        //     await publishEndpoint.Publish(new FeedItemUpdated(existingItem.Id, existingItem.Image, existingItem.Caption, existingItem.LikeCount));


        //     return NoContent();
        // }

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

            //await publishEndpoint.Publish(new FeedItemDeleted(item.Id));

            return NoContent();
        }
        #endregion
    }
}