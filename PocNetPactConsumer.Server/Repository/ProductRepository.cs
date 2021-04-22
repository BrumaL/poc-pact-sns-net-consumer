using PocNetPactConsumer.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace PocNetPactConsumer.Server.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> productList = new List<Product>
        { 
            new Product { Id = 1, Name = "Polestar 1" },
            new Product { Id = 2, Name = "Polestar 2" }
        };

        public Product GetProduct(int id)
        {
            return productList.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAllProducts()
        {
            return productList;
        }

        public void SaveProduct(Product product)
        {
            productList.Add(product);
        }
    }
}
