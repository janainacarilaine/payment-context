using Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand
            {
                FirstName = ""
            };

            command.Validate();
            Assert.AreEqual(false, command.IsValid);
        }
    }
}
