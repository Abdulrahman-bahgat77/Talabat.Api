using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;
        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        
        #region Without specification
        public async Task<IEnumerable<T>> GetAllSync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _dbcontext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }

      

        public async Task<T> GetByIdSync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);

            //return await _dbcontext.Set<T>().Where(P=>P.Id==id).Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

        }
        #endregion

        #region With Specification
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
             return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecSync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }

        
        #endregion
    }
}
