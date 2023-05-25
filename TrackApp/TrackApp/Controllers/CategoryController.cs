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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return BadRequest("Nonexistent id");
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Add(string name)
        {
            var category=_categoryService.Add(name);
            if (category == null)
                return BadRequest("Empty string sent");
            return Ok(category);
        }

        [HttpDelete]
        public ActionResult<Category> Remove(int id)
        {
            var category = _categoryService.Remove(id);
            if (category == null)
                return BadRequest("Nonexistent id");
            return Ok(category);
        }
    }
}

