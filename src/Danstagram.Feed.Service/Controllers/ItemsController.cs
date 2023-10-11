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

namespace Danstagram.Feed.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        #region Properties

        private readonly IRepository<FeedItem> feedItemsRepository;
        private readonly IRepository<AccountItem> accountItemsRepository;
        private readonly IRepository<LikeItem> likeItemsRepository;
        private readonly IPublishEndpoint publishEndpoint;

        #endregion

        #region Constructors

        public ItemsController(IRepository<FeedItem> feedItemsRepository, 
                                IRepository<AccountItem> accountItemsRepository, 
                                IRepository<InventoryItem> inventoryItemsRepository,
                                IPublishEndpoint publishEndpoint)
        {
            this.feedItemsRepository = feedItemsRepository;
            this.accountItemsRepository = accountItemsRepository;
            this.inventoryItemsRepository = inventoryItemsRepository;
            this.publishEndpoint = publishEndpoint;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemAsync()
        {
            List<ItemDto> items = new();
            var feedItems = await feedItemsRepository.GetAllAsync();
            var inventoryItems = await inventoryItemsRepository. GetAllAsync();
            var accountItems = await accountItemsRepository.GetAllAsync();
            var likeItems = await likeItemsRepository.GetAllAsync();
            foreach(var item in feedItems){
                var userId = (inventoryItems.Single(existingItem => existingItem.ItemId == item.Id)).UserId;
                var itemDto = new ItemDto(
                    ItemId = item.Id,
                    UserName = accountItems.Single(item => item.Id == userId),
                    Image = item.Image,
                    LikeCount = 
                );

            return items;
        }

        
        public async Task<IEnumerable<FeedItemDto>> GetFeedItemAsync()
        {
            IEnumerable<FeedItemDto> items = (await feedItemsRepository.GetAllAsync())
                                        .Select(item => item.AsDto());
            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            FeedItem item = await itemsRepository.GetAsync(id);

            return item == null ? (ActionResult<ItemDto>)NotFound() : (ActionResult<ItemDto>)item.AsDto();
        }

        // // Post /items
        // [HttpPost]
        // public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        // {
        //     FeedItem item = new()
        //     {
        //         Id = Guid.NewGuid(),
        //         Image = createItemDto.Image,
        //         Caption = createItemDto.Caption,
        //         LikeCount = 0,
        //         CreatedDate = DateTimeOffset.UtcNow
        //     };
        //     await itemsRepository.CreateAsync(item);

        //     await publishEndpoint.Publish(new FeedItemCreated(item.Id, item.Image, item.Caption, item.LikeCount));

        //     return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        // }

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

        // // DELETE /items/{id}
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync(Guid id)
        // {
        //     FeedItem item = await itemsRepository.GetAsync(id);

        //     if (item == null)
        //     {
        //         return NotFound();
        //     }
        //     await itemsRepository.RemoveAsync(item.Id);

        //     await publishEndpoint.Publish(new FeedItemDeleted(item.Id));

        //     return NoContent();
        // }
    }
}