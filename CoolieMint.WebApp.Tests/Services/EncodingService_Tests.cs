using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Tests.Services
{
    [TestClass]
    public class EncodingService_Tests
    {
        IEncodingService InitializeEncodíngService() => new EncodingService();

        [TestMethod]
        public void Encode_String()
        {
            var stringContent = "Test";
            var byteContent = new byte[4] { 84, 101, 115, 116 };
            var encoderService = InitializeEncodíngService();

            var encodedString = encoderService.Encode(stringContent);

            Assert.AreEqual(byteContent.Length, encodedString.Length);
            CollectionAssert.AreEquivalent(byteContent, encodedString);
        }

        [TestMethod]
        public void Decode_String()
        {
            var stringContent = "Test";
            var byteContent = new byte[4] { 84, 101, 115, 116 };
            var encoderService = InitializeEncodíngService();

            var decodedString = encoderService.Decode(byteContent);

            Assert.AreEqual(stringContent, decodedString);
        }
    }
}
