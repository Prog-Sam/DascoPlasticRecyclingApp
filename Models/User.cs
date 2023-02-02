namespace DascoPlasticRecyclingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<UserAccount> UserAccounts { get; set; }
    }
}
