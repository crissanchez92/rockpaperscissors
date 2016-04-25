using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPSClientApp.Models
{
    public class ChampionshipModel
    {
        [Display(Name = "Enter the campionship")]
        public string Input { get; set; }
        [Display(Name = "The winner is")]
        public string Winner { get; set; }
        [Display(Name = "The strategy was")]
        public string Strategy { get; set; }
        [Display(Name = "There was an error")]
        public string ErrorMessage { get; set; }
    }
}