using HPCL.Common.Models.ViewModel.Cards;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class ControlCardController : Controller
    {
        private readonly IControlCardSearch _cardSearch;

        public ControlCardController(IControlCardSearch cardSearch)
        {
            _cardSearch = cardSearch;
        }

        public async Task<IActionResult> ControlCardSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ControlCardSearch(ControlCardRequest entity)
        {
            var searchList = await _cardSearch.ControlCardSearch(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

    }
}
