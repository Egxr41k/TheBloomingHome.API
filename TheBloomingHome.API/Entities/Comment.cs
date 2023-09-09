namespace TheBloomingHome.API.Entities;

public class Comment
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
}
