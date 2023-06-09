﻿using System;
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
        private readonly IListService _listService;
        public ListController(IListService listService)
        {
            this._listService = listService;
        }

        [HttpGet]
        public ActionResult<List> GetCurrentWorkingList()
        {
            var listToGet = _listService.GetCurrentWorkingList();
            if (listToGet == null)
                return StatusCode(500,"Current working list not existent, server error");
            return Ok(listToGet);
        }

        [HttpGet]
        public ActionResult<List> GetList(int id)
        {
            var listToGet=_listService.GetList(id);
            if (listToGet == null)
                return BadRequest("Nonexistent id");
            return Ok(listToGet);
        }

        [HttpGet]
        public List<List> GetAllLists()
        {
            return _listService.GetAllLists();
        }

        [HttpPost]
        public ActionResult<List> CreateList()
        {
            return Ok(_listService.CreateList());
        }

        [HttpDelete]
        public ActionResult<List> DeleteList(int id)
        {
            var listToDelete=_listService.DeleteList(id);
            if (listToDelete == null)
                return BadRequest("Nonexistent id");
            return Ok(listToDelete);
        }
    }
}

