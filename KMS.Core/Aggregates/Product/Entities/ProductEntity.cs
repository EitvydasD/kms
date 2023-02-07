using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Product.Entities;
public class ProductEntity : BaseEntity, IAggregateRoot
{
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
