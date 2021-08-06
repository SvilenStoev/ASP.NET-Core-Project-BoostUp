namespace BoostUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Friendship
    {
        public int Id { get; init; }

        public string RequesterId { get; set; }

        public User Requester { get; set; }

        public string ResponderId { get; set; }

        public User Responder { get; set; }

        public bool IsAccepted { get; set; }
    }
}
