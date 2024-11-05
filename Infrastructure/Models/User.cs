namespace Infrastructure.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public bool Sex { get; set; }
    public string Description { get; set; }
    public string Role { get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}