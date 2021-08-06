namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.Post;

    public class Post
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public IEnumerable<Image> Images { get; set; } = new List<Image>();

        public string FromUserId { get; set; }

        public User FromUser { get; set; }

        [InverseProperty("ToPost")]
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
