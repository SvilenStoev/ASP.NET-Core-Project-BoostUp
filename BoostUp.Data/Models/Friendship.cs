namespace BoostUp.Data.Models
{
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
