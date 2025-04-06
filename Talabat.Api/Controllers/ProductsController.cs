using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Api.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }
        //Get All product
        //BaseUrl/api/Products ->GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await _productRepo.GetAllSync();
            return Ok(Products);
        }

        //Get product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetPorduct(int id)
        {
            var Product = await _productRepo.GetByIdSync(id);

            return Ok(Product);
        }

    }
}
