using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackApp.Core;
using TrackApp.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ListController : ControllerBase
    {
        IListService listService;
        public ListController(IListService listService)
        {
            this.listService = listService;
        }

        [HttpGet]
        public ActionResult<List> GetList(int id)
        {
            var listToGet=listService.GetList(id);
            if (listToGet == null)
                return BadRequest("Nonexistant id");
            return Ok(listToGet);
        }

        [HttpGet]
        public List<List> GetAllLists()
        {
            return listService.GetAllLists();
        }

        [HttpPost]
        public ActionResult<List> CreateList()
        {
            return Ok(listService.CreateList());
        }

        [HttpDelete]
        public ActionResult<List> DeleteList(int id)
        {
            var listToDelete=listService.DeleteList(id);
            if (listToDelete == null)
                return BadRequest("Nonexistant id");
            return Ok(listToDelete);
        }
    }
}

