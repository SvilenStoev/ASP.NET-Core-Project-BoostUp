namespace BoostUp.Services.Jobs.Models
{
    public class JobDetailsServiceModel : JobServiceModel
    {
        public string AddressText { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string RecruiterId { get; set; }

        public int EmploymentTypeId { get; set; }
    }
}
