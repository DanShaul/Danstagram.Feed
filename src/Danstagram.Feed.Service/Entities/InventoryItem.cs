using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities{
    public class InventoryItem : IEntity{
        public Guid Id{get;set;}
        public byte[] Image{get;set;}
        public string Caption{get;set;}
        public int LikeCount{get;set;}
        public DateTimeOffset CreateDate{get;set;}
    }
}