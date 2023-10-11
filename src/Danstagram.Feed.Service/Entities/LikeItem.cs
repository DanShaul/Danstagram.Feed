
using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities{
    public class LikeItem : IEntity
    {
        #region Properties
        
        public Guid Id { get;set;}
        public Guid UserId { get;set;}
        public Guid FeedItemId { get;set;}

        #endregion
    }
}