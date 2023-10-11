
using System;

namespace Danstagram.Feed.Service.Entities{
    public class LikeItem : IInteractionItem
    {
        #region Properties
        
        public Guid Id { get;set;}
        public Guid UserId { get;set;}
        public Guid ItemId { get;set;}

        #endregion
    }
}