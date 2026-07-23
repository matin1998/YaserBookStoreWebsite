namespace BookStore.Domain.Entities;

public class Book:BaseEntity
{
    public string BookTitle { get; set; }
    public int BookPrice { get; set; }
    public string? BookDescription { get; set; }
    public int BookInventory {  get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
}
