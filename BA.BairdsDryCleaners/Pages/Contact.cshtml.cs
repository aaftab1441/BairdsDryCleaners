using BA.BairdsDryCleaners.Adapters;
using BA.BairdsDryCleaners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using reCAPTCHA.AspNetCore;
using System.Threading.Tasks;

namespace BA.BairdsDryCleaners.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IRecaptchaService _recaptcha;

        [BindProperty]
        public ContactUsModel contactUsModel { get; set; }

        public ContactModel(IConfiguration config, IRecaptchaService recaptcha, ILogger<ContactModel> logger)
        {
            _config = config;
            _recaptcha = recaptcha;
            _logger = logger;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {


            if (ModelState.IsValid)
            {
                contactUsModel.EmailTemplateName = "ContactUsForm";
                RecaptchaResponse recaptcha = await _recaptcha.Validate(Request);
                if (!recaptcha.success)
                {
                    ModelState.AddModelError("Recaptcha", "There was an error validating the Recaptcha code.  Please try Again!");
                    return Page();
                }
                else
                {
                    ContactUsAdapter contactUs = new ContactUsAdapter(_config);
                    await contactUs.CreateAndSendEmail(contactUsModel);
                    return RedirectToPage("/ThankYou");
                }
            }
            else
            {
                return Page();
            }
        }
    }
}