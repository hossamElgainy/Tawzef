﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Tawzef.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Display(Name ="عنوان الوظيفه")]
        public string JobTitle { get; set; }
        [Display(Name = "وصف الوظيفه")]
        public string JobContent { get; set; }
        [Display(Name = "صوره الوظيفه")]
        public string JobImage { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}