using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
   public interface IGenericRepository<T> where T:BaseEntity
    {
        #region With out specification
        Task<IEnumerable<T>> GetAllSync();
        Task<T> GetByIdSync(int id);
        #endregion

        #region With Specification
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T> GetByIdWithSpecSync(ISpecifications<T> spec);
        #endregion
    }
}
