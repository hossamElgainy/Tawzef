using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tawzef.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "القاعدة")]
        public string Name { get; set; }
    }
}