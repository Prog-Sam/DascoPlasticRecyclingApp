using System.ComponentModel.DataAnnotations;

namespace DascoPlasticRecyclingApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public ContactType ContactType { get; set; }
    }
}
