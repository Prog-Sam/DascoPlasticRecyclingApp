using System.ComponentModel.DataAnnotations;

namespace DascoPlasticRecyclingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Position { get; set; }
        //public ICollection<Contact> Contacts { get; set; }
        //public ICollection<UserAccount> UserAccounts { get; set; }
    }
}
