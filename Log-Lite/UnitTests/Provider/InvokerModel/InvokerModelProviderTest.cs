using LibLite.Log.Lite.Exception;
using LibLite.Log.Lite.Model.Invoker;
using LibLite.Log.Lite.Provider.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibLite.Log.Lite.Tests.Provider.InvokerModel
{
    [TestClass]
    public class InvokerModelProviderTest
    {
        [TestMethod]
        public void ReturnsInvokerDirectlyFromFunction()
        {
            var invokerModelProvider = new InvokerModelProvider(1);

            var invokerModel = invokerModelProvider.GetCurrentInvoker();

            var expected = new Lite.Model.Invoker.InvokerModel(
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

            var expected = new Lite.Model.Invoker.InvokerModel(
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
