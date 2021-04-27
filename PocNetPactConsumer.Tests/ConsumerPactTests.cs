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
                            Some.String.Named("DataType").Like("number"),
                            Some.String.Named("StringValue").Like("3")
                            ),
                        Some.Object.Named("Name").With(
                            Some.String.Named("DataType").Like("string"),
                            Some.String.Named("StringValue").Like("Polestar 3")
                            ),
                        Some.Object.Named("Color").With(
                            Some.String.Named("DataType").Like("string"),
                            Some.String.Named("StringValue").Like("Red")
                            ),
                        Some.Object.Named("Price").With(
                            Some.String.Named("DataType").Like("float"),
                            Some.String.Named("StringValue").Like("10,00")
                            )
                        )
                    ));
        }


        [Fact]
        public void GetProductWithId1()
        {
            var expectedProduct = new Product() { Id = 1, Name = "Polestar 1", Color = "Blue" };

            var repo = new ProductRepository();
            var actualProduct = repo.GetProduct(1);

            Assert.Equal(JsonConvert.SerializeObject(expectedProduct), JsonConvert.SerializeObject(actualProduct));
        }


        [Fact]
        public async Task Handle_WhenProductIsCreated_SavesProduct()
        {
            var stubRepo = Substitute.For<IProductRepository>();
            var consumer = new ProductService(stubRepo);
            var productCreated = new Product { Id = 3, Name = "Polestar 3", Color = "Red", Price = 10.00M };

            var builder = new MessagePactBuilder("MartinsNetMessageConsumer", "MartinsMessageProvider");

            await builder.SetUp(_messageBuilder
                .VerifyConsumer<ProductCreated>(e => consumer.HandleSaveProductEvent(e)))
                .BuildAsync();
           stubRepo.Received(1).SaveProduct(productCreated);
        }
    }

//    tag-contract:
//    if: ${{ github.event.pull_request.state == 'closed'}
//}
//runs - on: ubuntu - latest
//    steps:
//-name: Checkout
// uses: actions / checkout@v2
// - name: Setup Node.js

//   uses: actions/setup-node @v1

//   with:

//     node-version: 14
//      - name: Install dependencies

//   run: yarn install --frozen-lockfile
//      - uses: ./.github/actions/create-release
//   with:

//     stage: dev
//     version: ${ { github.event.pull_request.head.sha } }
//pacticipant: $PACTICIPANT
}
