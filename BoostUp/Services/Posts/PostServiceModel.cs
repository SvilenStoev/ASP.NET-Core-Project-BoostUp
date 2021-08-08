namespace BoostUp.Services.Posts
{
    using System;
    using BoostUp.Services.Comments;
    using System.Collections.Generic;

    public class PostServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }

        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public string UserId { get; set; }

        public string FromUserProfileImagePath { get; set; }

        public int FromUserProfileImageId { get; set; }
      
        public DateTime CreatedOn { get; set; }

        public ICollection<CommentServiceModel> Comments { get; set; }
    }
}
