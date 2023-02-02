namespace DascoPlasticRecyclingApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactTypeId { get; set; }
        public string value { get; set; }
    }
}
