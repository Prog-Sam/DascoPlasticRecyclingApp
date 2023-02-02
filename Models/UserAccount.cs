namespace DascoPlasticRecyclingApp.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
