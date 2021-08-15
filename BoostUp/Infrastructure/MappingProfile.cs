namespace BoostUp.Infrastructure
{
    using AutoMapper;
    using BoostUp.Models.Jobs;
    using BoostUp.Models.Addresses;
    using BoostUp.Services.Jobs.Models;
    using BoostUp.Data.Models;
    using BoostUp.Services.Users.Models;
    using System.Linq;
    using BoostUp.Services.Companies.Models;
    using BoostUp.Models.Companies;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CompanyCategoryServiceModel>();

            this.CreateMap<Industry, CompanyIndustryServiceModel>();

            this.CreateMap<EmploymentType, JobEmploymentTypeServiceModel>();

            this.CreateMap<Company, CompanyServiceModel>()
                 .ForMember(c => c.Country, cfg => cfg.MapFrom(c => c.Address.Country))
                 .ForMember(c => c.City, cfg => cfg.MapFrom(c => c.Address.City))
                 .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Value))
                 .ForMember(c => c.IndustryName, cfg => cfg.MapFrom(c => c.Industry.Value));

            this.CreateMap<Company, CompanyDetailsServiceModel>()
                 .ForMember(c => c.Country, cfg => cfg.MapFrom(c => c.Address.Country))
                 .ForMember(c => c.City, cfg => cfg.MapFrom(c => c.Address.City))
                 .ForMember(c => c.AddressText, cfg => cfg.MapFrom(c => c.Address.AddressText))
                 .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Value))
                 .ForMember(c => c.IndustryName, cfg => cfg.MapFrom(c => c.Industry.Value))
                 .ForMember(c => c.JobsCount, cfg => cfg.MapFrom(c => c.Jobs.Count()))
                 .ForMember(c => c.EmployeesCount, cfg => cfg.MapFrom(c => c.Employees.Count()));

            this.CreateMap<JobDetailsServiceModel, JobInputModel>()
                 .ForMember(j => j.Address, cfg => cfg.MapFrom(jd => new AddressInputModel
                 {
                     Country = jd.Country,
                     City = jd.City,
                     AddressText = jd.AddressText
                 }));

            this.CreateMap<CompanyDetailsServiceModel, CompanyInputModel>()
                .ForMember(c => c.Address, cfg => cfg.MapFrom(cd => new AddressInputModel
                {
                    Country = cd.Country,
                    City = cd.City,
                    AddressText = cd.AddressText
                }));

            this.CreateMap<Job, JobDetailsServiceModel>()
                 .ForMember(j => j.Country, cfg => cfg.MapFrom(j => j.Address.Country))
                 .ForMember(j => j.City, cfg => cfg.MapFrom(j => j.Address.City))
                 .ForMember(j => j.AddressText, cfg => cfg.MapFrom(j => j.Address.AddressText))
                 .ForMember(j => j.UserId, cfg => cfg.MapFrom(j => j.Recruiter.UserId));

            this.CreateMap<User, ProfileServiceModel>()
                 .ForMember(p => p.CompanyName, cfg => cfg.MapFrom(u => u.Company.Name))
                 .ForMember(p => p.Country, cfg => cfg.MapFrom(u => u.Address.Country))
                 .ForMember(p => p.City, cfg => cfg.MapFrom(u => u.Address.City))
                 .ForMember(p => p.ProfileImagePath, cfg => cfg.MapFrom(u =>
                 GlobalConstants.GetProfileImagePath(u.ProfileImageId, u.ProfileImage.Extension, u.ProfileImage.ImageUrl, u.Gender)))
                 .ForMember(p => p.CoverImagePath, cfg => cfg.MapFrom(u =>
                 GlobalConstants.GetProfileImagePath(u.CoverImageId, u.CoverImage.Extension, u.CoverImage.ImageUrl, u.Gender)))
                 .ForMember(p => p.FullName, cfg => cfg.MapFrom(u => u.FirstName + " " + u.LastName))
                 .ForMember(p => p.UserId, cfg => cfg.MapFrom(u => u.Id))
                 .ForMember(p => p.Friends, cfg => cfg.MapFrom(u =>
                     u.FriendshipRequests.Where(fr => fr.IsAccepted).Count() +
                     u.FriendshipResponses.Where(fr => fr.IsAccepted).Count()));
        }
    }
}
