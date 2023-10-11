using Danstagram.Common;
using Danstagram.Feed.Service.Entities;

namespace Danstagram.Feed.Service.Consumers{
    public class RepositoryConsumer<T> where T : IEntity{
        #region Properties
        public readonly IRepository<T> repository;

        #endregion

        #region Constructors
        public RepositoryConsumer(IRepository<T> repository){
            this.repository = repository;
        }
        
        #endregion
    }
}