namespace BoostUp.Services.Users.Models
{
    using BoostUp.Data.Models;
    using BoostUp.Services.Friendships;

    public class ProfileServiceModel
    {
        public string SetProfileImageMessage { get; set; } = "Do you want to set it as a profile picture?";

        public string CurrentLoggedUser { get; set; }

        public string UserId { get; set; }

        public string JobTitle { get; set; }

        public GenderType Gender { get; set; }

        public string About { get; set; }

        public string Experience { get; set; }

        public string Education { get; set; }

        public int? CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string ProfileImagePath { get; set; }

        public string CoverImagePath { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int Friends { get; set; }

        public FriendshipServiceModel FriendShip { get; set; }
    }
}
