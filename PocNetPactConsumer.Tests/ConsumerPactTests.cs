using Newtonsoft.Json;
using PocNetPactConsumer.Server.Models;
using PocNetPactConsumer.Server.Repository;
using Xunit;
using ComPact.Builders.V3;
using ComPact.Models;
using NSubstitute;
using PocNetPactConsumer.Server.Services;
using System.Threading.Tasks;

namespace PocNetPactConsumer.Tests
{
    public class ConsumerPactTests
    {
        private MessageBuilder _messageBuilder;

        public ConsumerPactTests()
        {
            _messageBuilder = Pact.Message
                .Given(new ProviderState
                {
                })
                .ShouldSend("create product event")
                .With(Pact.JsonContent.With(
                    Some.String.Named("Message").Like("this is a message"),
                    Some.Object.Named("MessageAttributes").With(
                        Some.Object.Named("ID").With(
                            Some.Element.Named("DataType").Like("number"),
                            Some.Element.Named("StringValue").Like("3")
                            ),
                        Some.Object.Named("Name").With(
                            Some.Element.Named("DataType").Like("string"),
                            Some.Element.Named("StringValue").Like("Polestar 3")
                            )
                        )
                    ));
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
        public async Task Handle_WhenProductIsCreated_SavesProduct()
        {
            var stubRepo = Substitute.For<IProductRepository>();
            var consumer = new ProductService(stubRepo);
            var productCreated = new Product { Id = 3, Name = "Polestar 3" };

            var builder = new MessagePactBuilder("MartinsNetMessageConsumer", "MartinsMessageProvider");

            await builder.SetUp(_messageBuilder
                .VerifyConsumer<ProductCreated>(e => consumer.HandleSaveProductEvent(e)))
                .BuildAsync();
           stubRepo.Received(1).SaveProduct(productCreated);
        }

        // PactNet message-pact feature branch function
        //[Fact]
        //public void Handle_WhenProductIsCreated_SavesProduct()
        //{
        //    var stubRepo = Substitute.For<ProductRepository>();
        //    var consumer = new ProductService(stubRepo);
        //    var product = new Product { Id = 1, Name = "Polestar 1" };

        //    var providerStates = new[]
        //    {
        //        new ProviderState
        //        {
        //            Name = "There is a product"
        //        }
        //     };

        //    _messagePact.Given(providerStates)
        //        .ExpectedToReceive("create product event")
        //        .With(new Message
        //        {
        //            Contents = new
        //            {
        //                Message = "Some message",
        //                MessageAttributes = new Dictionary<string, MessageAttribute>()
        //                {   
        //                    { "ID", new MessageAttribute { DataType = "number", StringValue = "3" }},
        //                    { "Name", new MessageAttribute { DataType = "string", StringValue = "Polestar 3" }}
        //                }
        //            }
        //        })
        //        .VerifyConsumer<ProductCreated>(e => consumer.HandleSaveProductEvent(e));
        //    stubRepo.Received(1).SaveProduct(product);
        //}
    }
}
