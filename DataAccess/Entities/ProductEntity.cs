namespace Pryaniky.CRUD.DataAccess.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? OrderId { get; set; }
        public OrderEntity? Order { get; set; } = null;

    }
}
