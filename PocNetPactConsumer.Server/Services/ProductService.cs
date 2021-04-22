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

        public void HandleSaveProductEvent(ProductCreated @event)
        {
            Console.WriteLine(@event.MessageAttributes["Name"]);

            if (string.IsNullOrEmpty(@event.MessageAttributes["Name"].StringValue) )
            {
                throw new Exception("Product Name is required.");
            }

            if (!int.TryParse(@event.MessageAttributes["ID"].StringValue, out int id))
            {
                throw new Exception("Product Id is required.");
            }

            _productRepository.SaveProduct(new Product
            {
                Id = id,
                Name = @event.MessageAttributes["Name"].StringValue
            });
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
