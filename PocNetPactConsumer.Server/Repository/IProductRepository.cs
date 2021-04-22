using PocNetPactConsumer.Server.Models;
using System.Collections.Generic;

namespace PocNetPactConsumer.Server.Repository
{
    public interface IProductRepository
    {
        void SaveProduct(Product product);

        Product GetProduct(int id);

        List<Product> GetAllProducts();
    }
}
