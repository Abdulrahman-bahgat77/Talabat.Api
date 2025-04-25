using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
   public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        // CTOR for get all product
        public ProductWithBrandAndTypeSpecifications() :base()
        {
            Includes.Add(P=>P.ProductBrand);
            Includes.Add(P=>P.ProductType);
        }

        // Ctor For get product by id
        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P=> P.ProductType);
        }
    }
}
