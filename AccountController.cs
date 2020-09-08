using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClaimsManagementSystem.Models;

namespace ClaimsManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        ClaimContext C = new ClaimContext();
        // GET: Action
        [AllowAnonymous]
        public ActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserCredential User)
        {
            var user = C.UserCredentials.Find(User.UserId);
            if (ModelState.IsValid)
            {

                if (C.UserCredentials.Where(p => p.UserId == User.UserId && p.Password == User.Password && p.Role_Id == User.Role_Id && p.Status == "Approved").Any())
                {
                    if (User.Role_Id == 1)

                    {
                        FormsAuthentication.SetAuthCookie(User.UserId, false);
                        return RedirectToAction("RegisteredList", "Claim");
                    }

                    else
                    {
                        FormsAuthentication.SetAuthCookie(User.UserId, false);
                        return RedirectToAction("RaiseClaim", "Claim");
                    }
                }
                else if (C.UserCredentials.Where(p => p.UserId == User.UserId && p.Password == User.Password && p.Role_Id == User.Role_Id && p.Status == "Rejected").Any())
                {
                    return Content("Your Registration is Rejected");
                }
                else if (C.UserCredentials.Where(p => p.UserId == User.UserId && p.Password == User.Password && p.Role_Id == User.Role_Id && p.Status == null).Any())
                {
                    return Content("Your Registration is waiting for approval");
                }

                else
                {
                    ModelState.AddModelError("", "Enter valid User Id and Password");
                    return View(User);
                }
            }
            else
            {
                return View(User);
            }


        }

        [AllowAnonymous]
        public ActionResult Register()
        {

            var id = C.UserCredentials.Max(p => p.Constant);
            id += 1;
            string i = id.ToString();
            string userId = "2020_" + i;
            UserDetail U = new UserDetail();
            UserCredential newUser = new UserCredential();
            newUser.UserId = userId;
            U.UserCredential_UserId = userId;
            U.UserCredential = newUser;
            ViewBag.Roles = (C.Roles.Where(p => p.Id != 1));
            return View(U);
        }

        [HttpPost]
        public ActionResult Register(UserDetail User)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Details submitted successfully";

                UserCredential U = new UserCredential();
                U.UserId = User.UserCredential.UserId;
                U.Password = User.UserCredential.Password;
                U.Role_Id = User.UserCredential.Role_Id;
                C.UserCredentials.Add(U);
                C.SaveChanges();
                User.UserCredential_UserId = User.UserCredential.UserId;
                User.UserCredential = U;
                C.UserDetails.Add(User);
                C.SaveChanges();
                return RedirectToAction("LogIn", "Account");
                
            }
            else
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(User);
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");

        }

    }
}