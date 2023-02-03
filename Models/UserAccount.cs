using System.ComponentModel.DataAnnotations;

namespace DascoPlasticRecyclingApp.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Admin { get; set; }
        [Required]
        public User User { get; set; }
    }
}
