using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BA.BairdsDryCleaners.Adapters;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BA.BairdsDryCleaners.Models
{
    public class EmployeeFormModel : IContactForm
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }

        public List<IFormFile> Attachments { get; set; }
        public string EmailTemplateName { get; set; }

        //Present Address
        public string PresentAddressLine { get; set; }
        public string PresentCity { get; set; }
        public string PresentState { get; set; }
        public string PresentZIP { get; set; }
        //Perm Address
        public string PermanentAddressLine { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public string PermanentZIP { get; set; }
        //Employment Desired
        public string PositionApplied { get; set; }
        public string PositionStartDate{ get; set; }
        public string SalaryDesired { get; set; }
        public string CurrentlyEmployed { get; set; }
        public string InquireCurrentEmployer { get; set; }
        public string AppliedToBairds { get; set; }
        public string AppliedToBairdsWhereWhen { get; set; }
        //Education
        public string Highschool { get; set; }
        public string HighschoolYrsAttended { get; set; }
        public string HighschoolGraduated { get; set; }
        public string College { get; set; }
        public string CollegelYrsAttended { get; set; }
        public string CollegeGraduated { get; set; }
        public string CollegeMajor { get; set; }
        public string TradeBusiness { get; set; }
        public string TradeBusinessYrsAttended { get; set; }
        public string TradeBusinessGraduated { get; set; }
        public string TradeBusinessSubjects{ get; set; }
        //General Information
        public string SubjectsOfSpecialStudy { get; set; }
        public string SpecialSkillsTraining { get; set; }
        public string MilitaryService { get; set; }
        public string MilitaryRank { get; set; }
        //Current Employer
        public string CurrentEmployerNameAddress { get; set; }
        public string CurrentEmployerEmploymentDates { get; set; }
        public string CurrentEmployerPosition { get; set; }
        public string CurrentEmployerSalary { get; set; }
        public string CurrentEmployerReason { get; set; }
        public string CurrentEmployerContactName { get; set; }
        public string CurrentEmployerContactPhone { get; set; }
        //Former Employers

        public string FormerEmployerNameAddress1 { get; set; }
        public string FormerEmployerEmploymentDates1 { get; set; }
        public string FormerEmployerPosition1 { get; set; }
        public string FormerEmployerSalary1 { get; set; }
        public string FormerEmployerReason1 { get; set; }

        public string FormerEmployerNameAddress2 { get; set; }
        public string FormerEmployerEmploymentDates2 { get; set; }
        public string FormerEmployerPosition2 { get; set; }
        public string FormerEmployerSalary2 { get; set; }
        public string FormerEmployerReason2 { get; set; }

        public string FormerEmployerNameAddress3 { get; set; }
        public string FormerEmployerEmploymentDates3 { get; set; }
        public string FormerEmployerPosition3 { get; set; }
        public string FormerEmployerSalary3 { get; set; }
        public string FormerEmployerReason3 { get; set; }

        //References

        public string ReferenceName1 { get; set; }
        public string ReferenceAddressLine1 { get; set; }
        public string ReferenceCity1 { get; set; }
        public string ReferenceState1 { get; set; }
        public string ReferenceZIP1 { get; set; }
        public string ReferenceBusiness1 { get; set; }
        public string ReferenceYearsKnown1 { get; set; }

        public string ReferenceName2 { get; set; }
        public string ReferenceAddressLine2 { get; set; }
        public string ReferenceCity2 { get; set; }
        public string ReferenceState2 { get; set; }
        public string ReferenceZIP2 { get; set; }
        public string ReferenceBusiness2 { get; set; }
        public string ReferenceYearsKnown2 { get; set; }

        public string ReferenceName3 { get; set; }
        public string ReferenceAddressLine3 { get; set; }
        public string ReferenceCity3 { get; set; }
        public string ReferenceState3 { get; set; }
        public string ReferenceZIP3 { get; set; }
        public string ReferenceBusiness3 { get; set; }
        public string ReferenceYearsKnown3 { get; set; }

        //Comments
        public string Comments { get; set; }

    }
}
