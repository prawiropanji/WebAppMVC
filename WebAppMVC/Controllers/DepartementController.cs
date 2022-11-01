using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppMVC.Context;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class DepartementController : Controller
    {
        private MyContext _myContext;
        public DepartementController(MyContext myContext)
        {
            _myContext = myContext;

        }

        //Get All
        public IActionResult Index()
        {

            var data = _myContext.Departements.Include(d => d.Division).ToList();

            return View(data);
        }

        //GetBy ID
        public IActionResult Details(int id)
        {
            var data = _myContext.Departements.Find(id);
            return View(data);
            
        }

        //Insert Get POST
        public IActionResult Create()
        {
            //GenerateViewBagOptions();

            var divisions = _myContext.Divisions.ToList();

            var options = GenerateViewBagOptions();

            ViewBag.Options = options;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement departement)
        {
            _myContext.Departements.Add(departement);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Departement");
            }
            return View();
        }

        //Update GET POST
        public IActionResult Edit(int id)
        {
            var data = _myContext.Departements.Find(id);

            var options = GenerateViewBagOptions();

            ViewBag.Options = options;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Departement departement)
        {
            var data = _myContext.Departements.Find(id);
            if (data != null)
            {
                data.Name = departement.Name;
                data.DivisionId = departement.DivisionId;
                _myContext.Entry(data).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index", "Departement");
                }

            }
            return View();
        }

        //Delete GET POST
        public IActionResult Delete(int id)
        {
            var data = _myContext.Departements.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Departement departement)
        {
            _myContext.Departements.Remove(departement);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Departement");
            }
            return View();
        }

        [NonAction]
        private List<SelectListItem> GenerateViewBagOptions()
        {
            var divisions = _myContext.Divisions.ToList();

            List<SelectListItem> Options = new List<SelectListItem>();
            divisions.ForEach(d => Options.Add(new SelectListItem { Text = d.Name, Value = d.Id.ToString() }));
            return Options;

           
        }
    }
}
