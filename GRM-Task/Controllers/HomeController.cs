using GRM_Task.Models;
using GRM_Task.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GRM_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ItemRepository _itemRepository;
        public HomeController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["PairsCount"] = _itemRepository.GetAllPairs().Count();
            var items = _itemRepository.GetAllItems();
            return View(items);
        }
        [HttpGet]
        public IActionResult FormPartialView()
        {
            return PartialView("~/Views/Shared/_FormPartialView.cshtml",_itemRepository.GetNextPair()??null);
        }
        [HttpPost]
        public IActionResult Index([FromForm] CompareItemsModel comparedItems)
        {
            _itemRepository.CompareItems(comparedItems);
            ViewData["PairsCount"] = _itemRepository.GetAllPairs().Count();
            var items = _itemRepository.GetAllItems();
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
