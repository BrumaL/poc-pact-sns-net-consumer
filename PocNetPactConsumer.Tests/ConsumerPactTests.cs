using Newtonsoft.Json;
using NSubstitute;
using PactNet.PactMessage;
using PactNet.PactMessage.Models;
using PocNetPactConsumer.Server.Models;
using PocNetPactConsumer.Server.Repository;
using PocNetPactConsumer.Server.Services;
using System.Collections.Generic;
using Xunit;

namespace PocNetPactConsumer.Tests
{
    public class ConsumerPactTests : IClassFixture<PocNetMessagePact>
    {
        private readonly IMessagePact _messagePact;

        public ConsumerPactTests(PocNetMessagePact data)
        {
            _messagePact = data.MessagePact;
        }

        [Fact]
        public void GetProductWithId1()
        {
            var expectedProduct = new Product() { Id = 1, Name = "Polestar 1" };

            var repo = new ProductRepository();
            var actualProduct = repo.GetProduct(1);

            Assert.Equal(JsonConvert.SerializeObject(expectedProduct), JsonConvert.SerializeObject(actualProduct));
        }

        [Fact]
        public void Handle_WhenProductIsCreated_SavesProduct()
        {
            var stubRepo = Substitute.For<ProductRepository>();
            var consumer = new ProductService(stubRepo);
            var product = new Product { Id = 1, Name = "Polestar 1" };

            var providerStates = new[]
            {
                new ProviderState
                {
                    Name = "There is a product"
                }
             };

            _messagePact.Given(providerStates)
                .ExpectedToReceive("create product event")
                .With(new Message
                {
                    Contents = new
                    {
                        Message = "Some message",
                        MessageAttributes = new Dictionary<string, MessageAttribute>()
                        {   
                            { "ID", new MessageAttribute { DataType = "number", StringValue = "3" }},
                            { "Name", new MessageAttribute { DataType = "string", StringValue = "Polestar 3" }}
                        }
                    }
                })
                .VerifyConsumer<ProductCreated>(e => consumer.HandleSaveProductEvent(e));
            stubRepo.Received(1).SaveProduct(product);
        }
    }
}
