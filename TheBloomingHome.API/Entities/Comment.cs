namespace TheBloomingHome.API.Entities;

public class Comment
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Value { get; set; }
    //public DateTime? CreatedDate { get; set; } = DateTime.Now;
}
