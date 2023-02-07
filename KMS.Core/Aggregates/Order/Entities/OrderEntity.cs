using KMS.Core.Aggregates.Product.Entities;
using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Order.Entities;
public class OrderEntity : BaseEntity, IAggregateRoot
{
    public DateTime OrderDate { get; set; }
    
    public decimal Total { get; set; }
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
