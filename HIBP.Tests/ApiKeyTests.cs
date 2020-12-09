namespace HIBP.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ApiKeyTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(ArgumentException))]
        public void ApiKey_WithInvalidInput_Throws(string input)
        {
            new ApiKey(input);
        }
    }
}
