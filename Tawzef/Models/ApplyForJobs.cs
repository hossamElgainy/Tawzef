using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Tawzef.Models
{
    public class ApplyForJobs
    {
        public int Id { get; set; }
        [Display(Name = "نص الرساله")]
        public string Message { get; set; }
        [Display(Name = "تاريخ التقدم")]
        public DateTime ApplyDate { get; set; }
        [Display(Name = "الوظيفه")]
        public int JobId { get; set; }
        public string UserId { get; set; }



        public virtual Job job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}