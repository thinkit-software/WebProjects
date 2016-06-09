using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts145.DynamicWebAPI;
using ts145.DynamicWebAPI.Controllers;

namespace ts145.DynamicWebAPI.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // 정렬
            ValuesController controller = new ValuesController();

            // 동작
            IEnumerable<string> result = controller.Get();

            // 어설션
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // 정렬
            ValuesController controller = new ValuesController();

            // 동작
            string result = controller.Get(5);

            // 어설션
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // 정렬
            ValuesController controller = new ValuesController();

            // 동작
            controller.Post("value");

            // 어설션
        }

        [TestMethod]
        public void Put()
        {
            // 정렬
            ValuesController controller = new ValuesController();

            // 동작
            controller.Put(5, "value");

            // 어설션
        }

        [TestMethod]
        public void Delete()
        {
            // 정렬
            ValuesController controller = new ValuesController();

            // 동작
            controller.Delete(5);

            // 어설션
        }
    }
}
