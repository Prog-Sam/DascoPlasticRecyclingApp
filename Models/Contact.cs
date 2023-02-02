namespace DascoPlasticRecyclingApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string value { get; set; }

        public User User { get; set; }
        public ContactType ContactType { get; set; }
    }
}
