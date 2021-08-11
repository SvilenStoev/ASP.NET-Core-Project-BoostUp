namespace BoostUp.Services.Jobs.Models
{
    public class JobDetailsServiceModel : JobServiceModel
    {
        public string AddressText { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string RecruiterId { get; set; }

        public int EmploymentTypeId { get; set; }

        public int Views { get; set; }

        public string RecruiterEmail { get; set; }

        public string RecruiterPhoneNumber { get; set; }
        
        public string RecruiterFullName { get; set; }

        public string RecruiterCompanyName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyCategory { get; set; }
        
        public string CompanyIndustry { get; set; }
    }
}
