using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository;

namespace Talabat.Api.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,IMapper mapper)
        {
            _productRepo = ProductRepo;
             _mapper = mapper;
        }
        //Get All product
        //BaseUrl/api/Products ->GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec =new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MapperProduct=_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(Products);
            return Ok(MapperProduct);
        }

        //Get product By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetPorduct(int id)
        {
            var spec= new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecSync(spec);
            if (Product == null) return NotFound(new ApiResponse(404));
            var MapperProduct=_mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MapperProduct);
        }

    }
}
