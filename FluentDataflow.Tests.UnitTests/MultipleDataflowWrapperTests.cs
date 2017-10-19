using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks.Dataflow;
using Moq;
using System.Threading.Tasks;

namespace FluentDataflow.Tests.UnitTests
{
    [TestClass]
    public class MultipleDataflowWrapperTests
    {
        [TestMethod]
        public async Task TestMultipleDataflowWrapper()
        {
            var mockSourceBlock = new Mock<IDataflowBlock>();

            var target = new MultipleSourceDataflowWrapper(new[] { mockSourceBlock.Object });

            // test target.Complete()
            bool sourceCompleteCalled = false;
            mockSourceBlock.Setup(b => b.Complete()).Callback(() => sourceCompleteCalled = true);
            target.Complete();
            Assert.IsTrue(sourceCompleteCalled);

            // test target.Fault()
            bool sourceFaultCalled = false;
            mockSourceBlock.Setup(b => b.Fault(It.IsAny<Exception>())).Callback<Exception>(ex =>
            {
                sourceFaultCalled = true;

                Assert.IsNotNull(ex);
            });
            target.Fault(new Exception());
            Assert.IsTrue(sourceFaultCalled);

            // test target.Completion without error
            var task = Task.FromResult(222);
            mockSourceBlock.Setup(b => b.Completion).Returns(task);
            var resultTask = target.Completion;
            await resultTask;
            Assert.AreEqual(TaskStatus.RanToCompletion, resultTask.Status);
        }
    }
}
