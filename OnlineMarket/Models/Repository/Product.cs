namespace OnlineMarket.Models.Repository
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Category_id { get; set; }
        public int Brand_Id { get; set; }
    }
}
