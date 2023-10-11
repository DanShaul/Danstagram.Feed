
using System;

namespace Danstagram.Feed.Service.Entities{
    public class LikeItem : IInteractionItem
    {
        #region Properties
        
        public Guid UserId { get;set;}

        public Guid ItemId { get;set;}

        public Guid Id { get;set;}

        #endregion
    }
}