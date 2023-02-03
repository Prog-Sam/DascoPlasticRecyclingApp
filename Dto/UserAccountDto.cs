using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Dto
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public User User { get; set; }
    }
}
