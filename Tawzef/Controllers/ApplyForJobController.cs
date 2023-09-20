using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tawzef.Models;
using WebApplication1.Models;

namespace Tawzef.Controllers
{
    public class ApplyForJobController : Controller
    {
        /*public ApplyForJobController(ApplicationDbContext _db)
        {
            db = _db;
        }*/
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
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
            if (check.Count() < 1)
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

        [HttpPost]
        public ActionResult Edit(ApplyForJobs job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetUserJobs","Home");
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
                return RedirectToAction("GetUserJobs","Home");
            }
            catch
            {
                return View();
            }
        }

    }
}