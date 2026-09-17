using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListbycategory")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }   

    }
}