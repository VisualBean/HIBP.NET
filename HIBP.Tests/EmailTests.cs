namespace HIBP.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("a")]
        [DataRow("a@a")]
        [DataRow("a.com")]
        [DataRow("a@a.c")]
        [ExpectedException(typeof(ArgumentException))]
        public void Email_WithInvalidInput_Throws(string input)
        {
            new Email(input);
        }
    }
}
