using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesControllers : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesControllers(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _categoryService.GetList();
            
            if (result.success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        //[HttpGet("getListbycategory")]
        //public IActionResult GetListByCategory(int categoryId)
        //{
        //    var result = _categoryService.GetListByCategory(categoryId);
        //    if (result.success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    return BadRequest(result.Message);

        [HttpGet("getbyid")]
        public IActionResult GetById(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            if (result.success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            var result = _categoryService.Add(category);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Category category)
        {
            var result = _categoryService.Delete(category);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Category category)
        {
            var result = _categoryService.Update(category);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
