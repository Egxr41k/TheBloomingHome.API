namespace TheBloomingHome.API.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public string Email { get; set; }
    public List<Comment> Comments { get; set; } = new();

}
