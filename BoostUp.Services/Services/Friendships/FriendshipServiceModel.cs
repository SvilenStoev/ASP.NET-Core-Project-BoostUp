namespace BoostUp.Services.Friendships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FriendshipServiceModel
    {
        public bool IsAccepted { get; set; }

        public string RequesterId { get; set; }

        public string ResponderId { get; set; }
    }
}
