using PocNetPactConsumer.Server.Models;
using PocNetPactConsumer.Server.Repository;
using System;
using System.Collections.Generic;

namespace PocNetPactConsumer.Server.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void HandleSaveProductEvent(ProductCreated productCreated)
        {
            if (string.IsNullOrEmpty(productCreated.MessageAttributes["Name"].StringValue) )
            {
                throw new Exception("Product Name is required.");
            }

            if (!int.TryParse(productCreated.MessageAttributes["ID"].StringValue, out int id))
            {
                throw new Exception("Product Id is required.");
            }

            if (!decimal.TryParse(productCreated.MessageAttributes["Price"].StringValue, out decimal price))
            {
                throw new Exception("Price is required and can not be 0.");
            }

           

            _productRepository.SaveProduct(new Product
            {
                Id = id,
                Name = productCreated.MessageAttributes["Name"].StringValue,
                Color = productCreated.MessageAttributes["Color"].StringValue,
                Price = price
            });
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
