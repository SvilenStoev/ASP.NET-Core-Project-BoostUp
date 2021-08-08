namespace BoostUp.Infrastructure
{
    using AutoMapper;
    using BoostUp.Models.Jobs;
    using BoostUp.Models.Addresses;
    using BoostUp.Services.Jobs.Models;
    using BoostUp.Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<JobDetailsServiceModel, JobInputModel>()
              .ForMember(j => j.Address, cfg => cfg.MapFrom(jd => new AddressInputModel
              {
                  Country = jd.Country,
                  City = jd.City,
                  AddressText = jd.AddressText
              }));

            this.CreateMap<Job, JobDetailsServiceModel>()
                .ForMember(j => j.Country, cfg => cfg.MapFrom(j => j.Address.Country))
                .ForMember(j => j.City, cfg => cfg.MapFrom(j => j.Address.City))
                .ForMember(j => j.AddressText, cfg => cfg.MapFrom(j => j.Address.AddressText))
                .ForMember(j => j.UserId, cfg => cfg.MapFrom(j => j.Recruiter.UserId));
        }
    }
}
