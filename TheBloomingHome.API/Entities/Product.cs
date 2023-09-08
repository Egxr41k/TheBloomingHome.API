namespace TheBloomingHome.API.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public bool IsAvailable => Count > 0;
    public Feature[] Features { get; set; }
    public string[] Stats { get; set; }
    public Comment[] Comments { get; set; }
}

