using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Context;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class DivisionController : Controller
    {
        private MyContext _myContext;
        public DivisionController(MyContext myContext)
        {
            _myContext = myContext;
        }
        //Get All
        public IActionResult Index()
        {
            var data = _myContext.Divisions.ToList();
            return View(data);
        }

        //GetBy ID
        public IActionResult Details(int id)
        {
            var data = _myContext.Divisions.Find(id);
            return View(data);
        }

        //Insert Get POST
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Division division)
        {
            _myContext.Divisions.Add(division);
            var result = _myContext.SaveChanges();
            if(result > 0)
            {
                return RedirectToAction("Index", "Division");
            }
            return View();
        }

        //Update GET POST
        public IActionResult Edit(int id)
        {
            var data = _myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Division division)
        {
            var data = _myContext.Divisions.Find(id);
            if(data != null)
            {
                data.Name = division.Name;
                _myContext.Entry(data).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if(result > 0)
                {
                    return RedirectToAction("Index", "Division");
                }
                
            }
            return View();
        }

        //Delete GET POST
        public IActionResult Delete(int id) 
        {
            var data = _myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (Division division)
        {
            _myContext.Divisions.Remove(division);
            var result = _myContext.SaveChanges();
            if(result > 0)
            {
                return RedirectToAction("Index", "Division");
            }
            return View();
        }
    }

}
