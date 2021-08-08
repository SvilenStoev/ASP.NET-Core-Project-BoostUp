namespace BoostUp.Infrastructure
{
    using AutoMapper;
    using BoostUp.Models.Jobs;
    using BoostUp.Models.Addresses;
    using BoostUp.Services.Jobs.Models;
    using BoostUp.Data.Models;
    using BoostUp.Services.Users.Models;
    using System.Linq;

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

            this.CreateMap<User, ProfileServiceModel>()
                .ForMember(p => p.Country, cfg => cfg.MapFrom(u => u.Address.Country))
                .ForMember(p => p.City, cfg => cfg.MapFrom(u => u.Address.City))
                .ForMember(p => p.ProfileImagePath, cfg => cfg.MapFrom(u =>
                GlobalConstants.GetProfileImagePath(u.ProfileImageId, u.ProfileImage.Extension, u.ProfileImage.ImageUrl)))
                .ForMember(p => p.CoverImagePath, cfg => cfg.MapFrom(u =>
                GlobalConstants.GetProfileImagePath(u.CoverImageId, u.CoverImage.Extension, u.CoverImage.ImageUrl)))
                .ForMember(p => p.FullName, cfg => cfg.MapFrom(u => u.FirstName + " " + u.LastName))
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(u => u.Id))
                .ForMember(p => p.Friends, cfg => cfg.MapFrom(u =>
                    u.FriendshipRequests.Where(fr => fr.IsAccepted).Count() +
                    u.FriendshipResponses.Where(fr => fr.IsAccepted).Count()));
        }
    }
}
