using E_cart.Models;
using E_cart.DTO.ProductDto;
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
        private Response res;

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

        [HttpGet("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            var itm = await productService.GetProductsByCategory(categoryName);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }


        [HttpPost("PostItems")]
        public async Task<IActionResult> Post([FromForm] CreateProductDTO item)
        {
            var itm = await productService.Post(item);
            if (itm == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new {id = itm.Id},itm);
        }

        [HttpPut("PutItems")]
        public async Task<IActionResult> Put(int Id, UpdateProductDTO item)
        {
            var itm = await productService.Put(Id, item);
            if (itm == null)
            {
                return BadRequest("Inavlid Credential");
            }
            return Ok(itm);
        }

        [HttpDelete("DeleteItems")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var itm = await productService.Delete(Id);
                if (!itm)
                {
                    return BadRequest("Error");
                }
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(string imageFileName)
        {
            try
            {
                var itm = await productService.DeleteImage(imageFileName);
                if (!itm)
                {
                    return BadRequest("Error");
                }
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
