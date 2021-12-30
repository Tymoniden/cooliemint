using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Tests.Services
{
    [TestClass]
    public class JsonSerializerService_Tests
    {
        public static IJsonSerializerService InitializeService() => new JsonSerializerService();

        [TestMethod]
        public void Serialize_Object()
        {
            var service = InitializeService();
            var objectToSerialize = new { Test = "Hello World!" };

            var serialized = service.Serialize(objectToSerialize);

            Assert.AreEqual("{\"Test\":\"Hello World!\"}", serialized);
        }

        [TestMethod]
        public void Deseríalize_Object()
        {
            const string stringPropertyContent = "StringPropertyContent";
            var service = InitializeService();
            var stringContent = $"{{\"StringProperty\":\"{stringPropertyContent}\"}}";
            
            var serializedObject = service.Deserialize<Model>(stringContent);

            Assert.IsNotNull(serializedObject);
            Assert.AreEqual(stringPropertyContent, serializedObject.StringProperty);
            Assert.AreEqual(0, serializedObject.IntProperty); 
            Assert.IsNull(serializedObject.DateTimeProperty);
        }

        [TestMethod]
        public void Deseríalize_Full_Object()
        {
            const string stringPropertyContent = "StringPropertyContent";
            const int intPropertyContent = 2;
            const double doublePropertyContent = 3.14;
            DateTime dateTimePropertyContent = new DateTime(2021, 12, 20, 23, 54, 20);

            var service = InitializeService();
            var stringContent = $"{{\"StringProperty\":\"{stringPropertyContent}\",\"IntProperty\":\"2\",\"DoubleProperty\":\"3.14\",\"DateTimeProperty\":\"2021-12-20T23:54:20\"}}";

            var model = service.Deserialize<Model>(stringContent);

            Assert.IsNotNull(model);
            Assert.AreEqual(stringPropertyContent, model.StringProperty);
            Assert.AreEqual(intPropertyContent, model.IntProperty);
            Assert.AreEqual(doublePropertyContent, model.DoubleProperty);
            Assert.IsNotNull(model.DateTimeProperty);
            Assert.AreEqual(dateTimePropertyContent, model.DateTimeProperty.Value);
        }

        [TestMethod]
        public void Deserialize_Ignore_Case()
        {
            const string stringPropertyContent = "Test";
            var stringContent = $"{{\"stringproperty\":\"{stringPropertyContent}\"}}";
            var serializer = InitializeService();

            var model = serializer.Deserialize<Model>(stringContent);

            Assert.IsNotNull(model);
            Assert.AreEqual(stringPropertyContent, model.StringProperty);
        }
    }

    class Model
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public double DoubleProperty { get; set; }
        public DateTime? DateTimeProperty { get; set; }
    }
}