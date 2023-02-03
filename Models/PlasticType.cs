using System.ComponentModel.DataAnnotations;

namespace DascoPlasticRecyclingApp.Models
{
    public class PlasticType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ImageLocation { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public long Qty { get; set; }

    }
}
