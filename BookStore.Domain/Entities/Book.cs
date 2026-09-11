namespace BookStore.Domain.Entities;

public class Book:Product
{
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
