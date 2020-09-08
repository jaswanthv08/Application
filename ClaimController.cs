using ClaimsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ClaimsManagementSystem.Controllers
{[Authorize]
    public class ClaimController : Controller
    {
        ClaimContext Context = new ClaimContext();
        // GET: Claims
        [Authorize(Roles ="Admin")]
        public ActionResult RegisteredList()

        {
            var registeredList = Context.UserDetails.Include(p => p.UserCredential.Role).Where(ip => ip.UserCredential.Status == null).ToList();
            return View(registeredList);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ApproveRegistration(string id)
        {
            UserCredential U = Context.UserCredentials.Find(id);
            U.Status = "Approved";
            Context.SaveChanges();
            return RedirectToAction("RegisteredList", "Claim");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RejectRegistration(string id)
        {
            UserCredential U = Context.UserCredentials.Find(id);
            U.Status = "Rejected";
            Context.SaveChanges();
            return RedirectToAction("RegisteredList", "Claim");
        }
        
        public ActionResult RaiseClaim()
        {
            return View();
        }
    }
}