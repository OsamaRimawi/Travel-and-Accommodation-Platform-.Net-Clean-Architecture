using System.ComponentModel.DataAnnotations;

namespace TBP.Domain.Entites
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string PostOffice { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }
}
