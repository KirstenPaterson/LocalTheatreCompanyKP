using LocalTheatreCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalTheatreCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ManageUsers
        public ActionResult Index()
        {
            return View();
        }
    }
}