using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly HotelRoomBookingEntities1 db = new HotelRoomBookingEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustLogin() => View();
        [HttpPost]
        public ActionResult CustLogin(string username, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = db.CustomerLogins.FirstOrDefault(u => u.UserName == username && u.Password == password);
                    if (user != null)
                    {
                        //Session["UserId"] = user.UserId;
                        return RedirectToAction("CustomerDashboard", "AdminLogin");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid login credentials";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }
        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(CustomerLogin user)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var isEmailExist = db.CustomerLogins.Any(x => x.UserName == user.UserName);
                if (isEmailExist)
                {
                    ModelState.AddModelError("Username", "UserName already exists.");
                    return View(user);
                }

                // Proceed with registration logic if email doesn't exist
                var users = new CustomerLogin
                {
                    UserName = user.UserName,
                    Password = user.Password
                };

                db.CustomerLogins.Add(user);
                db.SaveChanges();

                // Redirect to some success page or login page
                return RedirectToAction("CustLogin");
            }

            return View(user);
        }

        public ActionResult AdminsLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminsLogin(AdminLogin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the admin exists in the database
                    var existingAdmin = db.AdminLogins.FirstOrDefault(a => a.UserName == admin.UserName && a.Password == admin.Password);

                    if (existingAdmin != null)
                    {

                        return RedirectToAction("AdminDashboard", "AdminLogin");
                    }
                    else
                    {
                        // If the credentials are incorrect
                        ViewBag.ErrorMessage = "Invalid email or password";
                        return View(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            // If the model state is not valid, return to the login view
            return View(admin);
        }
    }

}
