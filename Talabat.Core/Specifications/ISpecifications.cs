using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
 public   interface ISpecifications <T> where T:BaseEntity
    {
        //Sign for Property For Condition [Where(P=>P.id==id)]

        public Expression<Func<T,bool>> Criteria { get; set; }

        //sign for Include Property
        public List<Expression<Func<T,object>>> Includes { get; set; }
    }
}
