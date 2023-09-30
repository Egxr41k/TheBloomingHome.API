namespace TheBloomingHome.API.Entities
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public List<Feature> Features { get; set; } = new();
        public List<Property> Stats { get; set; } = new();
    }
}
