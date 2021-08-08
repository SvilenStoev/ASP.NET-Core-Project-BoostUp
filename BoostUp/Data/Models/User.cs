﻿namespace BoostUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        // Own properties
        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? ProfileImageId { get; set; }

        public Image ProfileImage { get; set; }

        public int? CoverImageId { get; set; }

        public Image CoverImage { get; set; }

        [MaxLength(AboutMaxLength)]
        public string About { get; set; }

        [MaxLength(ExperienceMaxLength)]
        public string Experience { get; set; }

        [MaxLength(EducationMaxLength)]
        public string Education { get; set; }

        [InverseProperty("FromUser")]
        public IEnumerable<Post> OwnPosts { get; set; } = new List<Post>();

        [InverseProperty("FromUser")]
        public IEnumerable<Comment> OwnComments { get; set; } = new List<Comment>();

        [InverseProperty("Requester")]
        public IEnumerable<Friendship> FriendshipRequests { get; set; } = new List<Friendship>();

        [InverseProperty("Responder")]
        public IEnumerable<Friendship> FriendshipResponses { get; set; } = new List<Friendship>();

        // Audit info
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
