using E_cart.Models;
using E_cart.Models.DTO;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        protected Response res;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
             res = new();
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> Get()
        {
            var itm = await productService.Get();
            if (itm == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "Some thing went wrong";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.Success = true;
            res.Result = itm;
            return Ok(res);
        }

        [HttpGet("GetProductsById")]
        public async Task<IActionResult> GetById(int id)
        {
            var itm = await productService.GetById(id);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpPost("PostItems")]
        public async Task<IActionResult> Post(CreateProductDTO item)
        {
            var itm = await productService.Post(item);
            if (itm == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new {id = itm.Id},itm);
        }
    }
}
