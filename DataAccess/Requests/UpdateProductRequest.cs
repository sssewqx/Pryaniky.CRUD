namespace Pryaniky.CRUD.DataAccess.Requests
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? OrderId { get; set; }
    }
}
