using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using WebAppMVC.Context;
using WebAppMVC.Models;

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
                .SingleOrDefault(u => u.Employee.Email == email && u.Password == password);

            if(user != null)
            {
                var responseLogin = new ResponseLogin(user.Employee.FullName, user.Employee.Email, user.Role.Name);


                return RedirectToAction("Index", "Home", responseLogin);
            
            }
            return View();

        }

    
        public IActionResult Register()
        {
            var options = GenerateViewBagOptions();
            ViewBag.Options = options;

            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, DateTime birthdate, string email, string password, int roleid)
        {
            _myContext.Employees.Add(new Employee(0, fullname, email, birthdate));
            var result = _myContext.SaveChanges();
            if(result > 0)
            {
                var employeeId = _myContext.Employees.SingleOrDefault(e => e.Email == email).Id;
                _myContext.Users.Add(new User(0, password, roleid, employeeId));
                var usersResult = _myContext.SaveChanges();
                if(usersResult > 0)
                {
                    return RedirectToAction("Login");
                }
                
            }
            return View();

        }

        [ActionName("Change_Password")]
        public IActionResult ChangePassword(string email)
        {
            var data = _myContext.Employees.Where(e => e.Email == email).Single();
            return View("ChangePassword", data);
        }


        [HttpPost]
        
        public IActionResult ChangePassword(string newpassword, string oldpassword, string email)
        {
            var user = _myContext.Users.Include(u => u.Employee).FirstOrDefault(u => u.Employee.Email.Equals(email));

            if (oldpassword.Equals(user.Password))
            {
                user.Password = newpassword;
                _myContext.Entry(user).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Login");
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
                user.Password = NewPassword;
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
