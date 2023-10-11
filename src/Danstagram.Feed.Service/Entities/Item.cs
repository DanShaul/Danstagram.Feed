using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities
{
    public class Item : IEntity {
        #region Properties
        public Guid Id { get; set; }
        public Guid UserName {get;set;}
        public byte[] Image { get; set; }
        
        public string Caption { get; set; }

        public DateTimeOffset CreatedDate{ get; set; }
        
        #endregion
    }
}