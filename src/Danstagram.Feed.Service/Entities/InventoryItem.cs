using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities{
    public class InventoryItem : IEntity{
        #region Properties
        
        public Guid Id {get;set;}

        public Guid UserId {get;set;}

        public Guid ItemId{get;set;}

        #endregion
    }
}