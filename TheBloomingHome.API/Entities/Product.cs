using System.Collections.Generic;

namespace TheBloomingHome.API.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public bool IsAvailable => Count > 0;
    public List<Feature> Features { get; set; } = new();
    public List<Property> Stats { get; set; } = new();
    //public List<Comment> Comments { get; set; } = new();
}

