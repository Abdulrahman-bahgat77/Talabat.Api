using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Errors;
using Talabat.Repository.Data;

namespace Talabat.Api.Controllers
{
     
    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext Dbcontext)
        {
            _dbcontext = Dbcontext;
        }

        [HttpGet ("NotFound")]

        public ActionResult GetNotFoundRequest()
        {
            var product = _dbcontext.Products.Find(100);
            if(product is  null) return NotFound(new ApiResponse(404));


            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var Product = _dbcontext.Products.Find(100);
            var ProductToReturn = Product.ToString();//Error
            return Ok(ProductToReturn);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetadRequest()
        {
            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")] 

        public ActionResult GetbadRequest(int id)
        {
            return Ok();
        }
    }
}
