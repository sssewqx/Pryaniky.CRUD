using Pryaniky.CRUD.Abstractions;
using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;
using Pryaniky.CRUD.DataAccess.Services;
using System.Xml.Linq;

namespace Pryaniky.CRUD.DataAccess
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {  
            using (var serviceScoped = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScoped.ServiceProvider.GetService<AppDbContext>();
                var orderService = serviceScoped.ServiceProvider.GetService<IOrderService>();
                context.Database.EnsureCreated();

                List<ProductEntity> productList;
                if (!context.Products.Any())
                {
                    productList = new List<ProductEntity>() 
                    {
                        new ProductEntity()
                        {   
                            Id = Guid.Parse("df548588-d9ce-4bab-983b-680454ec6baf"),           
                            Name = "Soap",
                            Description = "A substance used with water for washing and cleaning, made of a compound of natural oils or fats with sodium hydroxide or another strong alkali, and typically having perfume and colouring added.",
                            OrderId = null
                        },
                        new ProductEntity()
                        {   
                            Id = Guid.Parse("7fa3bcf8-d831-4e4f-b757-75829b32ab6c"),
                            Name = "Cheese",
                            Description = "Cheese is a concentrated dairy product made from fluid milk and is defined as the fresh or matured product obtained by draining the whey after coagulation of casein.",
                            OrderId = null
                        },
                        new ProductEntity()
                        {   
                            Id = Guid.Parse("24cb4b94-3c52-4664-9f78-688f37a008b1"),
                            Name = "Shovel",
                            Description = "An implement consisting of a broad blade or scoop attached to a long handle, used for taking up, removing, or throwing loose matter, as earth, snow, or coal.",
                            OrderId = null
                        },
                        new ProductEntity()
                        {   
                            Id = Guid.Parse("3f479a41-595f-4161-9013-9dc8be59a37e"),
                            Name = "Water Bottle",
                            Description = "A water bottle is a container that is used to hold liquids, mainly water, for the purpose of transporting a drink while travelling or while otherwise away from a supply of potable water.",
                            OrderId = null
                        },new ProductEntity()
                        {   
                            Id = Guid.Parse("e5171745-8419-47ac-93cf-9ac2f9bbd6af"),
                            Name = "Shirt",
                            Description = "A shirt is a cloth garment for the upper body (from the neck to the waist).",
                            OrderId = null
                        },
                    };
                    context.Products.AddRange(productList);
                    context.SaveChanges();
                }
                else
                {
                    productList = context.Products.ToList();
                }
                /////////////
                // ORDERS //
                ///////////
                List<OrderEntity> orderList;
                if (!context.Orders.Any())
                {
                    List<Guid> orderedProducts = productList.Select(p => p.Id).Take(3).ToList();
                    var request = new CreateOrderRequest();
                    request.ProductsId.AddRange(orderedProducts);
                    orderService.Create(request);

                }
                else
                {
                    orderList = context.Orders.ToList();  
                }
            }
        }        
    }
}
