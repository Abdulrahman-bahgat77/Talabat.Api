using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
 public static class SpecificationEvalutor<T> where T:BaseEntity
    {
        //Fun to build Query 
        //return await _dbcontext.Set<T>().Where(P=>P.Id==id).Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            var Query = inputQuery; //_dbcontext.Set<T>()
            if (spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);  //_dbcontext.Set<T>().Where(P=>P.Id==id)
            }

            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncudeExpression) => CurrentQuery.Include(IncudeExpression));
            return Query;
        }
    }
}
