using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Moq;

namespace FluentDataflow.Tests.UnitTests
{
    [TestClass]
    public class TargetDataflowWrapperTests
    {
        [TestMethod]
        public void TestTargetDataflowWrapper()
        {
            var mockOriginalTargetBlock = new Mock<ITargetBlock<int>>();
            var mockFinalTargetBlock = new Mock<IDataflowBlock>();

            var target = new TargetDataflowWrapper<int>(mockOriginalTargetBlock.Object, mockFinalTargetBlock.Object);

            // test target.Complete()
            bool originalCompleteCalled = false;
            mockOriginalTargetBlock.Setup(b => b.Complete()).Callback(() => originalCompleteCalled = true);
            target.Complete();
            Assert.IsTrue(originalCompleteCalled);

            // test target.Fault()
            bool originalFaultCalled = false;
            mockOriginalTargetBlock.Setup(b => b.Fault(It.IsAny<Exception>())).Callback<Exception>(ex =>
            {
                originalFaultCalled = true;

                Assert.IsNotNull(ex);
            });
            target.Fault(new Exception());
            Assert.IsTrue(originalFaultCalled);
        }
    }
}
