namespace BoostUp.Common
{
    public static class GlobalConstants
    {
        public class Cache
        {
            public const string allIndustriesCacheKey = nameof(allIndustriesCacheKey);
            public const string allCategoriesCacheKey = nameof(allCategoriesCacheKey);
            public const string allEmploymentTypesCacheKey = nameof(allEmploymentTypesCacheKey);
        }

        public static string[] AllowedImageExtensions => new[] { "jpg", "jpeg", "png", "gif", "tiff", "psd", "pdf", "eps" };

        public static string DefaultCompanyLogoPath => "/default pictures/Company Logo.jpg";

        public static string DefaultProfileImageMale => "Male Profile Picture.png";

        public static string DefaultProfileImageFemale => "Female Profile Picture.png";

        public static string DefaultProfileImageUnknown => "Unknown Profile Picture.png";

        public static string DefaultCoverImageName => "DefaultCoverImage.jpg";

        public static string GetProfileImagePath(int? imageId, string imageExtension, string imageUrl, GenderType gender)
        {
            if (imageUrl != null)
            {
                return imageUrl;
            }

            string localPath = string.Empty;

            if (imageId == null)
            {
                if (gender == GenderType.Male)
                {
                    localPath = $"/default pictures/{DefaultProfileImageMale}";
                }
                else if (gender == GenderType.Female)
                {
                    localPath = $"/default pictures/{DefaultProfileImageFemale}";
                }
                else
                {
                    localPath = $"/default pictures/{DefaultProfileImageUnknown}";
                }
            }
            else
            {
                localPath = "/default pictures/users/" + imageId + "." + imageExtension;
            }

            return localPath;
        }

        public static string GetCoverPath(string imageId, string imageExtension, string imageUrl)
        {
            if (imageUrl != null)
            {
                return imageUrl;
            }

            string localPath = string.Empty;

            if (imageId == null)
            {
                localPath = $"/images/{DefaultCoverImageName}";
            }
            else
            {
                localPath = "/images/users/" + imageId + "." + imageExtension;
            }

            return localPath;
        }

        public static string GetImagePath(string imageId, string imageExtension, string folder, string imageUrl)
        {
            if (imageUrl != null)
            {
                return imageUrl;
            }

            return $"{folder}/{imageId}.{imageExtension}";
        }
    }
}
