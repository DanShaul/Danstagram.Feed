using System;
using Danstagram.Common;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Danstagram.Feed.Service.Entities{
    public interface IInteractionItem : IEntity{
        #region Properties
        
        public Guid UserId {get; set;}

        public Guid ItemId {get; set;}

        #endregion
    }
}