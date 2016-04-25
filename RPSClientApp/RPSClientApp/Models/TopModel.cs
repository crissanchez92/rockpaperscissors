using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPSClientApp.Models
{
    public class TopModel
    {
        [Display(Name = "Enter the top count")]
        public int? Count { get; set; }
        [Display(Name = "Players")]
        public List<string> Players { get; set; }
    }
}