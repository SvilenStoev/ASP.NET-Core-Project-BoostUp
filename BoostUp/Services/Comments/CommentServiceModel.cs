namespace BoostUp.Services.Comments
{
    using System;

    public class CommentServiceModel
    {
        public string Content { get; set; }

        public string FromUsername { get; set; }

        public string FromUserId { get; set; }

        public string FromUserProfileImagePath { get; set; }

        public int? ToPostId { get; set; }

        public string ToImageId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
