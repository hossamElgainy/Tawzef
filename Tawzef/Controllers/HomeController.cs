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
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if(check.Count()<1)
            {
                var job = new ApplyForJobs();
                job.JobId = JobId;
                job.UserId = UserId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "لقد تم التسجيل بنجاح";
                return View();
            }
            else
            {
                ViewBag.result = "لقد تم التقدم لهذه الوظيفه";
                return View();
            }

        }
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
        public ActionResult GetUserJobs()
        {
            var userId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(z => z.UserId == userId).ToList();
            return View(Jobs);
        }
        public ActionResult MyJobDetails(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJobs job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetUserJobs");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplyForJobs job)
        {
            try
            {
                var MyApply = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(MyApply);
                db.SaveChanges();
                return RedirectToAction("GetUserJobs");
            }
            catch
            {
                return View();
            }
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