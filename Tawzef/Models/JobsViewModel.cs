using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tawzef.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJobs> Items { get; set; }
    }
}