using System.ComponentModel.DataAnnotations;

namespace DascoPlasticRecyclingApp.Models
{
    public class ContactType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public ICollection<Contact> Contacts { get; set; }
    }
}
