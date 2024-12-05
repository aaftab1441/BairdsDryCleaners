using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BA.BairdsDryCleaners.Models
{
    public class ContactUsModel : IContactForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string ReferredBy { get; set; }
        public List<IFormFile> Attachments { get; set; }

        [Required]
        public string Comments { get; set; }
        public string EmailTemplateName { get; set; }
    }
}
