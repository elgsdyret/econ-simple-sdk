using System.Diagnostics.Eventing.Reader;
using System.Linq;
using NUnit.Framework;

namespace econ_simple_sdk.examples
{
    [TestFixture]
    public class CreateCustomer
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
        public void CreateSimpleCustomer()
        {
            var debtorGroupId = client.DebtorGroup_GetAll().First();
            var termOfPaymentId = client.TermOfPayment_GetAll().First();
            
            var customerData = new DebtorData
            {
                Name = "MyCustomer",
                DebtorGroupHandle = debtorGroupId, // DebtorGroup is required, for this example we just use the first one. Note it is much more efficient to directly assign if you know the number: var someDebtorGrouId = new DebtorGroupHandle {Number = 12345};
                TermOfPaymentHandle = termOfPaymentId, // TermOfPayment is required, for this example we just use the first one. Note it is much more efficient to directly assign if you know the number: var someTermOfPaymentId = new TermOfPaymentHandle{Id = 12345}
                CurrencyHandle = new CurrencyHandle { Code = "DKK" }, // Currency is required, pick Swedish Kronar - the codes follows standard currency codes
                IsAccessible = true // you need to set this if you actually want to use the Customer
            };
            
            var customerId = client.Debtor_CreateFromData(customerData);
            Assert.That(customerId, Is.Not.Null);
        }


    }
}