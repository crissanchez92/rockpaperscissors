using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPSClientApp.Models
{
    public class SamplesModel
    {
        [Display(Name ="Sample forms")]
        public List<string> Samples { get; set; }
    }
}