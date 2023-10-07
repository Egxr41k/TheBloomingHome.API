namespace TheBloomingHome.API.Entities;

public class Property
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = "Додатково:";
    public string Value { get; set; }
}
