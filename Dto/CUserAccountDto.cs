using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Dto
{
    public class CUserAccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool Admin { get; set; }
        public User User { get; set; }
    }
}
