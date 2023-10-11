using System;
using Danstagram.Common;

namespace Danstagram.Feed.Service.Entities{
    public class AccountItem : IEntity
    {
        #region Properties

        public Guid Id{get;set;}
        public Guid UserId{get;set;}

        public string UserName{get;set;}

        public string Password{get;set;}

        #endregion

    }
}
