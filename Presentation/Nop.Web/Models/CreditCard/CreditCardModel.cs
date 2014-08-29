using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Nop.Web.Models.CreditCard
{
    public class CreditCardModel
    {
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