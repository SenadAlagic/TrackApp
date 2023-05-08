using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackApp.Core;
using TrackApp.Service;


namespace TrackApp.Controllers
{
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            return Ok(categoryService.GetAll());
        }

        [HttpGet]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = categoryService.GetById(id);
            if (category == null)
                return BadRequest("Nonexistant id");
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Add(string name)
        {
            return Ok(categoryService.Add(name));
        }

        [HttpDelete]
        public ActionResult<Category> Remove(int id)
        {
            var category = categoryService.Remove(id);
            if (category == null)
                return BadRequest("Nonexistant id");
            return Ok(category);
        }
    }
}

