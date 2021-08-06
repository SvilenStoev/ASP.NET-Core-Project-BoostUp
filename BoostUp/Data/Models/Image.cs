namespace BoostUp.Data.Models
{
    public class Image
    {
        public int Id { get; init; }

        public string ImageUrl { get; set; }

        public string Extension { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }
    }
}
