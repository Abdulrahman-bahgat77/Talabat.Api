using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
   public  class StoreContextSeed
    {
        //Seeding  
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                //Seeding Brands
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/Dataseed/brands.json");

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.ProductTypes.Any())
            {
                //seeding Types
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/Dataseed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Products.Any())
            {
                //Seeding Product

                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/Dataseed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
