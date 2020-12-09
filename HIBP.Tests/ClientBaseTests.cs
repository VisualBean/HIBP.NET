using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIBP.Tests
{
    [TestClass]
    public class ClientBaseTests : ClientBase
    {
        public ClientBaseTests() : base(new ApiKey("123"), "tests")
        {

        }

        [TestMethod]
        public void ClientBase_Constructor_SetsClientHeaders()
        {
            Assert.IsTrue(Client.DefaultRequestHeaders.Contains(UserAgentHeader));
            Assert.IsTrue(Client.DefaultRequestHeaders.Contains(HIBPApiKeyHeader));
        }
    }
}
