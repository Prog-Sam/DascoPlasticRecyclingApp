namespace DascoPlasticRecyclingApp.Dto
{
    public class PlasticTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageLocation { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }
    }
}
