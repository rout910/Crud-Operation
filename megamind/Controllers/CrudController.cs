using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using megamind.Models;
using megamind.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace megamind.Controllers
{
    //[Route("[controller]")]
    public class CrudController : Controller
    {
        private readonly CrudRepository _repo;
        private readonly ILogger<CrudController> _logger;

        public CrudController(ILogger<CrudController> logger, CrudRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var crud = _repo.GetAll();
            return Json(crud);
        }

        public IActionResult Add(tblcrud crud)
        {
            _repo.Insert(crud);
            return Json(new { message = "Data added successfully." });
        }

        public IActionResult Edit(tblcrud crud)
        {
            _repo.Update(crud);
            return Json(new { message = "Data Update successfully." });
        }

        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Json(new { message = "Data Delete successfully." });
        }

        public IActionResult GetAllStates()
        {
            var states = _repo.GetallState();
            return Json(states);
        }

        public IActionResult GetAllCitys(int id)
        {
            var city = _repo.GetAllCity(id);
            return Json(city);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}