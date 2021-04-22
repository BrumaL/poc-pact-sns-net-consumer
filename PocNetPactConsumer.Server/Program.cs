using PocNetPactConsumer.Server.Models;
using PocNetPactConsumer.Server.Repository;
using PocNetPactConsumer.Server.Services;
using System;
using System.Collections.Generic;

namespace PocNetPactConsumer.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService(new ProductRepository());

            var productEvent = new ProductCreated
            {
                Message = "Some message",
                MessageAttributes = new Dictionary<string, MessageAttribute>()
                {
                    { "ID", new MessageAttribute { DataType = "number", StringValue = "3" }},
                    { "Name", new MessageAttribute { DataType = "string", StringValue = "Polestar 3" }}
                }
            };


            productService.HandleSaveProductEvent(productEvent);

            var products = productService.GetAllProducts();
            foreach (var p in products) 
            {
                Console.WriteLine(p.Id);
                Console.WriteLine(p.Name);
            }
            
        }
    }
}
