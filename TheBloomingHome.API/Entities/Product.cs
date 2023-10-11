namespace TheBloomingHome.API.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageSrc { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public bool IsAvailable => Count > 0;
    public int NewPrice { get; set; }
    public int OldPrice { get; set; }
    public bool IsSale => NewPrice < OldPrice;
}

