using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow.Tests.UnitTests
{
    [TestClass]
    public class MultipleSourceDataflowBuilderTests
    {
        [TestMethod]
        public void TestMultipleSourceDataflowBuilder()
        {
            var mockSourceBlock = new Mock<ISourceBlock<int>>();

            var target = new MultipleSourceDataflowBuilder<int>(new[] { mockSourceBlock.Object });

            // test target.LinkToTarget
            bool finalSourceLinkToCalled = false;
            var mockTargetBlock = new Mock<ITargetBlock<int>>();
            mockSourceBlock.Setup(b => b.LinkTo(It.IsAny<ITargetBlock<int>>(), It.IsAny<DataflowLinkOptions>())).Callback(() => finalSourceLinkToCalled = true);
            var builder1 = target.LinkToTarget(mockTargetBlock.Object, null, null) as DataflowBuilder;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder1);
            Assert.IsInstanceOfType(builder1.OriginalSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder1.CurrentSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.AreEqual(mockTargetBlock.Object, builder1.TargetBlock);
            Assert.IsTrue(builder1.PropagateCompletion.GetValueOrDefault());

            // test target.LinkToPropagator
            finalSourceLinkToCalled = false;
            var mockPropagatorBlock = new Mock<IPropagatorBlock<int, int>>();
            var builder2 = target.LinkToPropagator(mockPropagatorBlock.Object, null, null) as SourceDataflowBuilder<int>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder2);
            Assert.IsInstanceOfType(builder2.OriginalSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder2.CurrentSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.AreEqual(mockPropagatorBlock.Object, builder2.FinalSourceBlock);
            Assert.IsTrue(builder2.PropagateCompletion.GetValueOrDefault());

            // test target.Batch
            finalSourceLinkToCalled = false;
            var builder3 = target.Batch(2, default(DataflowBatchOptions)) as SourceDataflowBuilder<int[]>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder3);
            Assert.IsInstanceOfType(builder3.OriginalSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder3.CurrentSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder3.FinalSourceBlock, typeof(BatchBlock<int>));
            Assert.IsTrue(builder3.PropagateCompletion.GetValueOrDefault());

            // test target.WriteOnce
            finalSourceLinkToCalled = false;
            var builder4 = target.WriteOnce(i => i, default(DataflowWriteOnceOptions)) as SourceDataflowBuilder<int>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder4);
            Assert.IsInstanceOfType(builder4.OriginalSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder4.CurrentSourceBlock, typeof(MultipleSourceDataflowWrapper));
            Assert.IsInstanceOfType(builder4.FinalSourceBlock, typeof(WriteOnceBlock<int>));
            Assert.IsTrue(builder4.PropagateCompletion.GetValueOrDefault());
        }
    }
}
