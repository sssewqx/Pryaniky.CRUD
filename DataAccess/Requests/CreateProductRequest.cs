using System.ComponentModel.DataAnnotations;

namespace Pryaniky.CRUD.DataAccess.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
