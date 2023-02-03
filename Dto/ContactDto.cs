using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public User User { get; set; }
        public ContactType ContactType { get; set; }
    }
}
