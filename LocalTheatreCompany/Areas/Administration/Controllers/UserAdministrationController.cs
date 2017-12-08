using LocalTheatreCompany.Models;
using LocalTheatreCompany.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalTheatreCompany.Areas.Administration.Controllers
{
    public class UserAdministrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administration/UserAdministration
        public ActionResult Index()
        {
            List<UserViewModel> theUsers = new List<UserViewModel>();
            foreach(ApplicationUser aUser in db.Users)
            {
                UserViewModel uViewModel = new UserViewModel();
                uViewModel.Id = aUser.Id;
                uViewModel.UserName = aUser.UserName;
                uViewModel.Email = aUser.Email;
                uViewModel.IsAdmin = aUser.IsAdmin;
                uViewModel.IsSuspended = aUser.IsSuspended;
                theUsers.Add(uViewModel);

            }
            return View(theUsers);
        }

        // GET: Administration/UserAdministration/Details/5
        public ActionResult Details(string id)
        {
            ApplicationUser aUser = db.Users.Single(m => m.Id == id);
            UserViewModel uvm = new ViewModel.UserViewModel();
            uvm.UserName = aUser.UserName;
            uvm.Email = aUser.Email;
            uvm.IsAdmin = aUser.IsAdmin;
            uvm.IsSuspended = aUser.IsSuspended;

            return View(uvm);
        }

        // GET: Administration/UserAdministration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/UserAdministration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administration/UserAdministration/Edit/5
        public ActionResult Edit(string id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel uvm = new UserViewModel();
            ApplicationUser applicationUser = db.Users.Find(id);
            if(applicationUser == null)
            {
                return HttpNotFound();

            }else
            {
                uvm.UserName = applicationUser.UserName;
                uvm.Email = applicationUser.Email;
                uvm.IsAdmin = applicationUser.IsAdmin;
                uvm.IsSuspended = applicationUser.IsSuspended;
            }
            return View(uvm);
        }

        // POST: Administration/UserAdministration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser au = db.Users.Find(userViewModel.Id);
            au.Id = userViewModel.Id;
            au.UserName = userViewModel.UserName;
            au.Email = userViewModel.Email;
            au.IsAdmin = userViewModel.IsAdmin;
            au.IsSuspended = userViewModel.IsSuspended;

           


            if (ModelState.IsValid)
            {
                if ((au.IsAdmin) && (!UserManager.IsInRole(au.Id, "Admin")))
                    UserManager.AddToRole(au.Id, "Admin");
                else if ((!au.IsAdmin) && (UserManager.IsInRole(au.Id, "Admin")))
                    UserManager.RemoveFromRoles(au.Id, "Admin");
                if ((au.IsSuspended) && (!UserManager.IsInRole(au.Id, "Limited")))
                    UserManager.AddToRole(au.Id, "Limited");
                else if ((!au.IsSuspended) && (UserManager.IsInRole(au.Id, "Limted")))
                    UserManager.RemoveFromRoles(au.Id, "Limited");


                db.Entry(au).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userViewModel);

        }

        // GET: Administration/UserAdministration/Delete/5
        public ActionResult Delete(string id)
        {
            UserViewModel uvm = new UserViewModel();
            ApplicationUser applicationUser = db.Users.Find(id);
            if(applicationUser == null)
            {
                return HttpNotFound();
            }else
            {
                uvm.Id = applicationUser.Id;
                uvm.UserName = applicationUser.UserName;
                uvm.Email = applicationUser.Email;
                uvm.IsAdmin = applicationUser.IsAdmin;
                uvm.IsSuspended = applicationUser.IsSuspended;

            }
            return View(uvm);
        }

        // POST: Administration/UserAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
