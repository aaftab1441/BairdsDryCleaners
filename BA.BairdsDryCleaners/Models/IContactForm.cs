using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.BairdsDryCleaners.Models
{
    public interface IContactForm
    {
        //Name
        public string Name { get; set; }
        //Email
        public string Email { get; set; }
        //Phone
        public string Phone { get; set; }
        public string EmailTemplateName { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
