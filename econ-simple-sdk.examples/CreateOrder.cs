using System;
using System.Linq;
using NUnit.Framework;

namespace econ_simple_sdk.examples
{
    [TestFixture]
    public class CreateOrder
    {
        private EconomicWebService client;

        [TestFixtureSetUp]
        public void Setup()
        {            
            client = TestApiClient.Connect();
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            client.Disconnect();
        }

        [Test]
        public void CreateOrderWithSingleLineWithNewProduct()
        {
            // create a new product
            var productData = new ProductData
            {
                Number = "Unique Product", // we need a unique product Number - running this twice will not work!
                ProductGroupHandle = client.ProductGroup_GetAll().First(), // all products need a product group - we just take the first - if none exist one needs to be created
                // if you know the id of a specific product group it is more efficient to use new ProductGroupHandle {Number = 123};            
                Name = "Test Product",
                IsAccessible = true // we have to remember to ensure that the product is not barred
            };
            var productId = client.Product_CreateFromData(productData);

            // create a new order            
            var orderData = new OrderData
            {
                Date = DateTime.Now, // a date is required - we just use current day
                DebtorHandle = client.Debtor_GetAll().First(), // all orders need a debtor - we just take the first - if none exist one needs to be created
                // if you know the id of a specific debtor it is more efficient to use var new DebtorHandle {Number = "some number"};
                DebtorName = "not used", // even though we have already specified the id of a debtor the name is also required - it is not used for anything though
                TermOfPaymentHandle = client.TermOfPayment_GetAll().First(), // all orders need a term of payment - we just take the first - if none exist one needs to be created
                // if you know the id of a specific term of payment it is more efficient to use new TermOfPaymentHandle{ Id = 123 }
                CurrencyHandle = new CurrencyHandle { Code = "DKK" } // use Danish kroner for currency - follows standard currency codes
            };
            var orderId = client.Order_CreateFromData(orderData);

            // add a line to our newly created order
            var orderLineData = new OrderLineData
            {
                OrderHandle = orderId, // use the order we have created
                ProductHandle = productId, // use the product we have created
                Quantity = 1 // we need to set the quantity
            };
            // use CreateFromDataArray as opposed to CreateFromData as we often want to create more than one line
            client.OrderLine_CreateFromDataArray(new[] {orderLineData});


        }
    }
}
