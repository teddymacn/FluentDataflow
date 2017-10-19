using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;

namespace FluentDataflow.Tests.UnitTests
{
    [TestClass]
    public class DataflowWrapperTests
    {
        [TestMethod]
        public async Task TestDataflowWrapper()
        {
            var mockOriginalSourceBlock = new Mock<IDataflowBlock>();
            var mockCurrentSourceBlock = new Mock<IDataflowBlock>();
            var mockTargetBlock = new Mock<IDataflowBlock>();

            var target = new DataflowWrapper(mockOriginalSourceBlock.Object, mockCurrentSourceBlock.Object, mockTargetBlock.Object, true);

            // test target.Complete()
            bool originalCompleteCalled = false;
            mockOriginalSourceBlock.Setup(b => b.Complete()).Callback(() => originalCompleteCalled = true);
            target.Complete();
            Assert.IsTrue(originalCompleteCalled);

            // test target.Fault()
            bool originalFaultCalled = false;
            mockOriginalSourceBlock.Setup(b => b.Fault(It.IsAny<Exception>())).Callback<Exception>(ex =>
            {
                originalFaultCalled = true;

                Assert.IsNotNull(ex);
            });
            target.Fault(new Exception());
            Assert.IsTrue(originalFaultCalled);

            // test target.Completion without error
            var task = Task.FromResult(0);
            mockCurrentSourceBlock.Setup(b => b.Completion).Returns(task);
            bool targetCompleteCalled = false;
            mockTargetBlock.Setup(b => b.Complete()).Callback(() => targetCompleteCalled = true);
            var task2 = Task.FromResult(222);
            mockTargetBlock.Setup(b => b.Completion).Returns(task2);
            var resultTask = target.Completion;
            await resultTask;
            Assert.IsTrue(targetCompleteCalled);
            Assert.AreEqual(222, ((resultTask as Task<Task>).Result as Task<int>).Result);

            // test target.Completion with error
            bool targetFalutCalled = false;
            mockTargetBlock.Setup(b => b.Fault(It.IsAny<Exception>())).Callback<Exception>(ex =>
            {
                targetFalutCalled = true;
                Assert.IsNotNull(ex);
            });
            var task3 = Task.FromException(new Exception());
            mockCurrentSourceBlock.Setup(b => b.Completion).Returns(task3);
            await target.Completion;
            Assert.IsTrue(targetFalutCalled);
        }
    }
}
