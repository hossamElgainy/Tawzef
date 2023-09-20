using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tawzef.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
           var job = db.Jobs.Find(JobId);
            if (job == null)
                return HttpNotFound();
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize(Roles = "الناشر")]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserId
                       select app;
            var grouped = from j in jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };
            return View(grouped.ToList());
        }
        [Authorize(Roles = "مستخدم")]
        public ActionResult GetUserJobs()
        {
            var userId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(z => z.UserId == userId).ToList();
            ViewBag.ModelCount = Jobs.Count();
            return View(Jobs);
        }
               
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string SearchName)
        {
            var jobs = db.Jobs.Where(a => a.JobTitle.Contains(SearchName)
                        || a.JobContent.Contains(SearchName)
                        || a.Category.CategoryName.Contains(SearchName)).ToList();
            return View(jobs);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}