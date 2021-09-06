namespace BoostUp.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class FriendshipInputModel
    {
        public string FromId { get; set; }

        [Required]
        public string ToId { get; set; }
    }
}
