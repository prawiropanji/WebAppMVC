using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using WebAppMVC.Context;
using WebAppMVC.Models;
using WebAppMVC.Utils;

namespace WebAppMVC.Controllers
{
    public class AccountController : Controller
    {

        private MyContext _myContext;
        public AccountController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Login()
        {

           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            //check authentication
            
            var user = _myContext.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .SingleOrDefault(u => u.Employee.Email == email);

            if (user != null)
            {
                if(Hashing.ValidatePassword(password, user.Password))
                {
                    HttpContext.Session.SetInt32("Id", user.Id);
                    HttpContext.Session.SetString("FullName", user.Employee.FullName);
                    HttpContext.Session.SetString("Email", user.Employee.Email);
                    HttpContext.Session.SetString("Role", user.Role.Name);

                    return RedirectToAction("Index", "Home");
                }
                

            }
            return View();

        }

    
        public IActionResult Register()
        {
           

            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, DateTime birthdate, string email, string password)
        {
            //check if email exist
            var user = _myContext.Employees.SingleOrDefault(e => e.Email == email);
            if (user == null)
            {
                _myContext.Employees.Add(new Employee(0, fullname, email, birthdate));
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    var employeeId = _myContext.Employees.SingleOrDefault(e => e.Email == email).Id;
                    _myContext.Users.Add(new User(0, Hashing.HashPassword(password), 1, employeeId));
                    var usersResult = _myContext.SaveChanges();
                    if (usersResult > 0)
                    {
                        return RedirectToAction("Login");
                    }

                }
            }
            return RedirectToAction("Register");

        }

        [ActionName("Change_Password")]
        public IActionResult ChangePassword()
        {
            string email = HttpContext.Session.GetString("Email");
            var data = _myContext.Employees.Where(e => e.Email == email).SingleOrDefault();
            return View("ChangePassword", data);
        }


        [HttpPost]
 
        public IActionResult ChangePassword(string newpassword, string oldpassword)
        {
            string email = HttpContext.Session.GetString("Email");
            var user = _myContext.Users.Include(u => u.Employee).FirstOrDefault(u => u.Employee.Email.Equals(email));

            if (Hashing.ValidatePassword(oldpassword, user.Password))
            {
                user.Password = Hashing.HashPassword(newpassword);
                _myContext.Entry(user).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string NewPassword, string FullName, string Email )
        {
            var user = _myContext.Users.Include(u => u.Employee).SingleOrDefault(u => u.Employee.Email == Email && u.Employee.FullName == FullName);
            
            if(user != null)
            {
                user.Password = Hashing.HashPassword(NewPassword);
                _myContext.Entry(user).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if(result > 0)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [NonAction]
        private List<SelectListItem> GenerateViewBagOptions()
        {
            var roles = _myContext.Roles.ToList();

            List<SelectListItem> Options = new List<SelectListItem>();
            roles.ForEach(r => Options.Add(new SelectListItem { Text = r.Name, Value = r.Id.ToString() }));
            return Options;


        }







    }
}
