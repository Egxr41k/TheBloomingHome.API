namespace TheBloomingHome.API.Entities;

public class Feature
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string ImageSrc { get; set; }
    public string Description { get; set; }
}
