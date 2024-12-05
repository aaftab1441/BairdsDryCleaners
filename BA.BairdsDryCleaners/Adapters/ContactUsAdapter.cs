using BA.BairdsDryCleaners.Models;
using BA.Common;
using BA.Common.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BA.BairdsDryCleaners.Adapters
{
    public class ContactUsAdapter
    {
        private readonly IConfiguration _config;
        private readonly EmailModel _email;
        private readonly IEmailService _emailService;

        public ContactUsAdapter(IConfiguration config)
        {
            _config = config;
            _emailService = new EmailService(_config);
            _email = new EmailModel();
        }

        public async Task CreateAndSendEmail(IContactForm contactUs)
        {
            try
            {
                _email.ReplyToName = contactUs.Name;
                _email.ReplyToAddress = contactUs.Email;

                //Get email body and subject
                string body = GetEmailTemplate(contactUs.EmailTemplateName);
                _email.Subject = GetEmailSubject(body);

                //Replace Tokens
                _email.Body = ReplaceTokens(contactUs, body);

                //Transfer Attachments into Email Model
                _email.Attachments = contactUs.Attachments;

                await SendMailAsync();

            }
            catch (Exception e)
            {
                throw new Exception("ContactUsAdapter.Create", e);
            }
        }

        private MemoryStream StreamToMemory(FileStream fileStream)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.SetLength(fileStream.Length);
            fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);

            memoryStream.Flush();
            fileStream.Close();
            memoryStream.Close();
            return memoryStream;
        }

        private string GetEmailTemplate(string EmailTemplateName)
        {
            EmailTemplateModel templateModel = new EmailTemplateModel();
            _config.GetSection(string.Format("EmailTemplates:{0}", EmailTemplateName)).Bind(templateModel);
            _email.IsHTML = templateModel.TemplateType == "HTML";

            string body = null;
            FileStream file = new FileStream(string.Format(@"Templates\\{0}", templateModel.TemplateName), FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }
            return (body);
        }

        private string GetEmailSubject(string body)
        {
            Match match = Regex.Match(body, @"<title>\s*(.+?)\s*</title>*");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        private string ReplaceTokens(IContactForm ContactUs, string Body)
        {
            StringBuilder sb = new StringBuilder();

            if (ContactUs is ContactUsModel contact)
            {
                sb.Append(Body.Replace(@"[Name]", contact.Name)
                    .Replace(@"[Email]", contact.Email)
                    .Replace(@"[Phone]", contact.Phone)
                    .Replace(@"[Page]", contact.EmailTemplateName)
                    .Replace(@"[Comments]", contact.Comments)
                    .Replace(@"[Referral]", contact.ReferredBy));
            }
            else if (ContactUs is PickUpModel pickup)
            {
                sb.Append(Body.Replace(@"[Name]", pickup.Name)
                    .Replace(@"[Email]", pickup.Email)
                    .Replace(@"[Phone]", pickup.Phone)
                    .Replace(@"[Page]", pickup.EmailTemplateName)
                    .Replace(@"[Address]", pickup.Address)
                    .Replace(@"[License]", pickup.DriversLicense)
                    .Replace(@"[Birthdate]", pickup.DateOfBirth)
                    .Replace(@"[City]", pickup.City)
                    .Replace(@"[BillingAddress]", pickup.BillingAddress)
                    .Replace(@"[ZIP]", pickup.ZIP)
                    .Replace(@"[Starch]", pickup.Starch)
                    .Replace(@"[Repairs]", pickup.Repairs)
                    .Replace(@"[Delivery]", pickup.Delivery)
                    .Replace(@"[Comments]", pickup.Comments)
                    .Replace(@"[Referral]", pickup.ReferredBy));
            }
            else if (ContactUs is EmployeeFormModel employee)
            {
                sb.Append(Body.Replace(@"[Name]", employee.Name)
                    .Replace(@"[Email]", employee.Email)
                    .Replace(@"[Phone]", employee.Phone)
                    .Replace(@"[Page]", employee.EmailTemplateName)
                    .Replace(@"[PresentAddressLine]", employee.PresentAddressLine)
                    .Replace(@"[PresentCity]", employee.PresentCity)
                    .Replace(@"[PresentState]", employee.PresentState)
                    .Replace(@"[PresentZIP]", employee.PresentZIP)

                    .Replace(@"[PermanentAddressLine]", employee.PermanentAddressLine)
                    .Replace(@"[PermanentCity]", employee.PermanentCity)
                    .Replace(@"[PermanentState]", employee.PermanentState)
                    .Replace(@"[PermanentZIP]", employee.PermanentZIP)

                    .Replace(@"[PositionApplied]", employee.PositionApplied)
                    .Replace(@"[PositionStartDate]", employee.PositionStartDate)
                    .Replace(@"[SalaryDesired]", employee.SalaryDesired)
                    .Replace(@"[SalaryDesired]", employee.SalaryDesired)
                    .Replace(@"[CurrentlyEmployed]", employee.CurrentlyEmployed)
                    .Replace(@"[InquireCurrentEmployer]", employee.InquireCurrentEmployer)
                    .Replace(@"[AppliedToBairds]", employee.AppliedToBairds)
                    .Replace(@"[AppliedToBairdsWhereWhen]", employee.AppliedToBairdsWhereWhen)

                    .Replace(@"[Highschool]", employee.Highschool)
                    .Replace(@"[HighschoolYrsAttended]", employee.HighschoolYrsAttended)
                    .Replace(@"[HighschoolGraduated]", employee.HighschoolGraduated)
                    .Replace(@"[College]", employee.College)
                    .Replace(@"[CollegelYrsAttended]", employee.CollegelYrsAttended)
                    .Replace(@"[CollegeGraduated]", employee.CollegeGraduated)
                    .Replace(@"[CollegeMajor]", employee.CollegeMajor)
                    .Replace(@"[TradeBusiness]", employee.TradeBusiness)
                    .Replace(@"[TradeBusinessYrsAttended]", employee.TradeBusinessYrsAttended)
                    .Replace(@"[TradeBusinessGraduated]", employee.TradeBusinessGraduated)
                    .Replace(@"[TradeBusinessSubjects]", employee.TradeBusinessSubjects)

                    .Replace(@"[SubjectsOfSpecialStudy]", employee.SubjectsOfSpecialStudy)
                    .Replace(@"[SpecialSkillsTraining]", employee.SpecialSkillsTraining)
                    .Replace(@"[MilitaryService]", employee.MilitaryService)
                    .Replace(@"[MilitaryRank]", employee.MilitaryRank)

                    .Replace(@"[CurrentEmployerNameAddress]", employee.CurrentEmployerNameAddress)
                    .Replace(@"[CurrentEmployerEmploymentDates]", employee.CurrentEmployerEmploymentDates)
                    .Replace(@"[CurrentEmployerPosition]", employee.CurrentEmployerPosition)
                    .Replace(@"[CurrentEmployerSalary]", employee.CurrentEmployerSalary)
                    .Replace(@"[CurrentEmployerReason]", employee.CurrentEmployerReason)
                    .Replace(@"[CurrentEmployerContactName]", employee.CurrentEmployerContactName)
                    .Replace(@"[CurrentEmployerContactPhone]", employee.CurrentEmployerContactPhone)

                    .Replace(@"[FormerEmployerNameAddress1]", employee.FormerEmployerNameAddress1)
                    .Replace(@"[FormerEmployerEmploymentDates1]", employee.FormerEmployerEmploymentDates1)
                    .Replace(@"[FormerEmployerPosition1]", employee.FormerEmployerPosition1)
                    .Replace(@"[FormerEmployerSalary1]", employee.FormerEmployerSalary1)
                    .Replace(@"[FormerEmployerReason1]", employee.FormerEmployerReason1)

                    .Replace(@"[FormerEmployerNameAddress2]", employee.FormerEmployerNameAddress2)
                    .Replace(@"[FormerEmployerEmploymentDates2]", employee.FormerEmployerEmploymentDates2)
                    .Replace(@"[FormerEmployerPosition2]", employee.FormerEmployerPosition2)
                    .Replace(@"[FormerEmployerSalary2]", employee.FormerEmployerSalary2)
                    .Replace(@"[FormerEmployerReason2]", employee.FormerEmployerReason2)

                    .Replace(@"[FormerEmployerNameAddress3]", employee.FormerEmployerNameAddress3)
                    .Replace(@"[FormerEmployerEmploymentDates3]", employee.FormerEmployerEmploymentDates3)
                    .Replace(@"[FormerEmployerPosition3]", employee.FormerEmployerPosition3)
                    .Replace(@"[FormerEmployerSalary3]", employee.FormerEmployerSalary3)
                    .Replace(@"[FormerEmployerReason3]", employee.FormerEmployerReason3)


                    .Replace(@"[ReferenceName1]", employee.ReferenceName1)
                    .Replace(@"[ReferenceAddressLine1]", employee.ReferenceAddressLine1)
                    .Replace(@"[ReferenceCity1]", employee.ReferenceCity1)
                    .Replace(@"[ReferenceState1]", employee.ReferenceState1)
                    .Replace(@"[ReferenceZIP1]", employee.ReferenceZIP1)
                    .Replace(@"[ReferenReferenceBusiness1ceZIP1]", employee.ReferenceBusiness1)
                    .Replace(@"[ReferenceYearsKnown1]", employee.ReferenceYearsKnown1)

                    .Replace(@"[ReferenceName2]", employee.ReferenceName2)
                    .Replace(@"[ReferenceAddressLine2]", employee.ReferenceAddressLine2)
                    .Replace(@"[ReferenceCity2]", employee.ReferenceCity2)
                    .Replace(@"[ReferenceState2]", employee.ReferenceState2)
                    .Replace(@"[ReferenceZIP2]", employee.ReferenceZIP2)
                    .Replace(@"[ReferenReferenceBusiness1ceZIP2]", employee.ReferenceBusiness2)
                    .Replace(@"[ReferenceYearsKnown2]", employee.ReferenceYearsKnown2)

                    .Replace(@"[ReferenceName3]", employee.ReferenceName3)
                    .Replace(@"[ReferenceAddressLine3]", employee.ReferenceAddressLine3)
                    .Replace(@"[ReferenceCity3]", employee.ReferenceCity3)
                    .Replace(@"[ReferenceState3]", employee.ReferenceState3)
                    .Replace(@"[ReferenceZIP3]", employee.ReferenceZIP3)
                    .Replace(@"[ReferenReferenceBusiness1ceZIP3]", employee.ReferenceBusiness3)
                    .Replace(@"[ReferenceYearsKnown3]", employee.ReferenceYearsKnown3)
                    );
            }
            return sb.ToString();
        }

        private async Task SendMailAsync()
        {
            try
            {
                await _emailService.SendMail(_email);
            }
            catch (Exception e)
            {
                throw new Exception("ContactUsAdapter failed to send email", e);
            }
        }
    }
}

