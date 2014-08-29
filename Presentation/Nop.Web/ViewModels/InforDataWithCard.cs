using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.ViewModels
{
    public class InforDataWithCard
    {
        public InforDataWithCard()
        {
            AvailableLevels = new List<SelectListItem>();
        }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { set; get; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { set; get; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter number")]
        public string ByEOID { set; get; }
        [Required(ErrorMessage = "byEO ID is required.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter number")]
        public string SharingPartnerID { set; get; }
        public string Level { set; get; }
        [Required(ErrorMessage = "VaidCode is required.")]
        public string VaidCode { set; get; }
        public IList<SelectListItem> AvailableLevels { get; set; }
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = " Email is required.")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [CreditCardAttribute]
        // [CreditCard]
        [DisplayName("Credit Card Number")]
        [Required(ErrorMessage = "Card number is required.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter number")]
        public string CreditCardNumber
        {
            get;
            set;
        }
    }
}