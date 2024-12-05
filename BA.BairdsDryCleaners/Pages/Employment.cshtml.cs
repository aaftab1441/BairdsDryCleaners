﻿using BA.BairdsDryCleaners.Adapters;
using BA.BairdsDryCleaners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using reCAPTCHA.AspNetCore;
using System.Threading.Tasks;

namespace BA.BairdsDryCleaners.Pages
{
    public class EmploymentModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRecaptchaService _recaptcha;

        [BindProperty]
        public EmployeeFormModel EmployeeForm { get; set; }

        public EmploymentModel(IConfiguration config, IRecaptchaService recaptcha)
        {
            _config = config;
            _recaptcha = recaptcha;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                EmployeeForm.EmailTemplateName = "EmployeeForm";
                RecaptchaResponse recaptcha = await _recaptcha.Validate(Request);
                if (!recaptcha.success)
                {
                    ModelState.AddModelError("Recaptcha", "There was an error validating the Recaptcha code.  Please try Again!");
                    return Page();
                }
                else
                {
                    ContactUsAdapter contactUs = new ContactUsAdapter(_config);
                    await contactUs.CreateAndSendEmail(EmployeeForm);
                    return RedirectToPage("/ThankYouEmployment");
                }
            }
            else
            {
                return Page();
            }
        }
    }
}