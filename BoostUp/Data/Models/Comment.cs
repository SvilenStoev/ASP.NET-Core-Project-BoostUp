namespace BoostUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Comment;

    public class Comment
    {
        public int Id { get; init; }

        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public string FromUserId { get; set; }

        public User FromUser { get; set; }

        public int? ToPostId { get; set; }

        public Post ToPost { get; set; }
    }
}
