namespace Infrastructure.Models;

public class Like
{
    public int Id { get; set; }
    public int DogOwner { get; set; }
    public int DogLiked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
} 