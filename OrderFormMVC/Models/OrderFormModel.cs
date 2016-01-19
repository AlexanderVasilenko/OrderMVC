using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OrderForm.Models
{
    public class OrderFormModel
    {
        [Required(ErrorMessage = "Please Enter your Name")]
        public string Name { get; set; }

        public bool IsPacked { get; set; }
        public int? Country { get; set; }

        [Required(ErrorMessage = "Please Enter your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter your Country")]
        public List<SelectListItem> Countries { get; set; }
        [Required(ErrorMessage = "Please Enter your Address")]
        public string Address { get; set; }
    }
}