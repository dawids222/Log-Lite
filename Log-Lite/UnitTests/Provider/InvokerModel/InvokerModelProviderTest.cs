using Log_Lite.Exception;
using Log_Lite.Model;
using Log_Lite.Provider.InvokerModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Provider.InvokerModel
{
    [TestClass]
    public class InvokerModelProviderTest
    {
        [TestMethod]
        public void ReturnsInvokerDirectlyFromFunction()
        {
            var invokerModelProvider = new InvokerModelProvider(1);

            var invokerModel = invokerModelProvider.GetCurrentInvoker();

            var expected = new Log_Lite.Model.InvokerModel(
                "InvokerModelProviderTest",
                "ReturnsInvokerDirectlyFromFunction"
            );
            AssertInvokersAreEqual(expected, invokerModel);
        }

        [TestMethod]
        public void ReturnsInvokerFromAnotherFunction()
        {
            var invokerModelProvider = new InvokerModelProvider(2);

            Func<IInvokerModel> func = () => invokerModelProvider.GetCurrentInvoker();
            var invokerModel = func();

            var expected = new Log_Lite.Model.InvokerModel(
                "InvokerModelProviderTest",
                "ReturnsInvokerFromAnotherFunction"
            );
            AssertInvokersAreEqual(expected, invokerModel);
        }

        [TestMethod]
        [ExpectedException(typeof(InvokerNotFoundExpection))]
        public void ReturnsInvokerNotFoundExpection()
        {
            var invokerModelProvider = new InvokerModelProvider(100);

            invokerModelProvider.GetCurrentInvoker();
        }

        public void AssertInvokersAreEqual(IInvokerModel expected, IInvokerModel actual)
        {
            Assert.AreEqual(expected.Method, actual.Method);
            Assert.AreEqual(expected.Class, actual.Class);
        }
    }
}
