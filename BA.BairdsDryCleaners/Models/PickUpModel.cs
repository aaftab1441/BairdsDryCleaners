using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BA.BairdsDryCleaners.Models
{
    public class PickUpModel : IContactForm
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        public string Phone { get; set; }
        public string EmailTemplateName { get; set; }
        public string ReferredBy { get; set; }
        public List<IFormFile> Attachments { get; set; }

        [Required(ErrorMessage = "Comments is required!")]
        public string Comments { get; set; }

        [Required]
        public string City { get; set; }


        [Required]
        public string ZIP { get; set; }
        public string Address { get; set; }
        public string DriversLicense { get; set; }

        [Required(ErrorMessage = "Billing Address is required!")]
        public string BillingAddress { get; set; }
        public string Starch { get; set; }
        public string Delivery { get; set; }
        public string Repairs { get; set; }
        public string DateOfBirth { get; set; }

    }
}
