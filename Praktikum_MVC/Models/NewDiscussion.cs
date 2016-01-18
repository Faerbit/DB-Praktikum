using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Praktikum_MVC.Models
{
    public class NewDiscussion
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
    }
}