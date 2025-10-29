namespace BookStore.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string BookTitle { get; set; }
    public int BookPrice { get; set; }
    public string? BookDescription { get; set; }
    public int BookInventory {  get; set; }
    public int CategoryId { get; set; }
}
