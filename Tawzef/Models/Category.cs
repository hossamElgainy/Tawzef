using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tawzef.Models
{
    public class Category
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="نوع الوظيفه")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفه")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}