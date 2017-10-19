using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks.Dataflow;
using FluentDataFlow;

namespace FluentDataflow.Tests.UnitTests
{
    [TestClass]
    public class SourceDataflowBuilderTests
    {
        [TestMethod]
        public void TestSourceDataflowBuilder()
        {
            var mockOriginalSourceBlock = new Mock<IDataflowBlock>();
            var mockCurrentSourceBlock = new Mock<IDataflowBlock>();
            var mockFinalSourceBlock = new Mock<ISourceBlock<int>>();

            var target = new SourceDataflowBuilder<int>(mockOriginalSourceBlock.Object, mockCurrentSourceBlock.Object, mockFinalSourceBlock.Object, true);

            // test target.LinkToTarget
            bool finalSourceLinkToCalled = false;
            var mockTargetBlock = new Mock<ITargetBlock<int>>();
            mockFinalSourceBlock.Setup(b => b.LinkTo(It.IsAny<ITargetBlock<int>>(), It.IsAny<DataflowLinkOptions>())).Callback(() => finalSourceLinkToCalled = true);
            var builder1 = target.LinkToTarget(mockTargetBlock.Object, null, null) as DataflowBuilder;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder1);
            Assert.AreEqual(mockOriginalSourceBlock.Object, builder1.OriginalSourceBlock);
            Assert.AreEqual(mockFinalSourceBlock.Object, builder1.CurrentSourceBlock);
            Assert.AreEqual(mockTargetBlock.Object, builder1.TargetBlock);
            Assert.IsTrue(builder1.PropagateCompletion.GetValueOrDefault());

            // test target.LinkToPropagator
            finalSourceLinkToCalled = false;
            var mockPropagatorBlock = new Mock<IPropagatorBlock<int, int>>();
            var builder2 = target.LinkToPropagator(mockPropagatorBlock.Object, null, null) as SourceDataflowBuilder<int>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder2);
            Assert.AreEqual(mockOriginalSourceBlock.Object, builder2.OriginalSourceBlock);
            Assert.AreEqual(mockFinalSourceBlock.Object, builder2.CurrentSourceBlock);
            Assert.AreEqual(mockPropagatorBlock.Object, builder2.FinalSourceBlock);
            Assert.IsTrue(builder2.PropagateCompletion.GetValueOrDefault());

            // test target.Batch
            finalSourceLinkToCalled = false;
            var builder3 = target.Batch(2, default(DataflowBatchOptions)) as SourceDataflowBuilder<int[]>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder3);
            Assert.AreEqual(mockOriginalSourceBlock.Object, builder3.OriginalSourceBlock);
            Assert.AreEqual(mockFinalSourceBlock.Object, builder3.CurrentSourceBlock);
            Assert.IsInstanceOfType(builder3.FinalSourceBlock, typeof(BatchBlock<int>));
            Assert.IsTrue(builder3.PropagateCompletion.GetValueOrDefault());

            // test target.WriteOnce
            finalSourceLinkToCalled = false;
            var builder4 = target.WriteOnce(i => i, default(DataflowWriteOnceOptions)) as SourceDataflowBuilder<int>;
            Assert.IsTrue(finalSourceLinkToCalled);
            Assert.IsNotNull(builder4);
            Assert.AreEqual(mockOriginalSourceBlock.Object, builder4.OriginalSourceBlock);
            Assert.AreEqual(mockFinalSourceBlock.Object, builder4.CurrentSourceBlock);
            Assert.IsInstanceOfType(builder4.FinalSourceBlock, typeof(WriteOnceBlock<int>));
            Assert.IsTrue(builder4.PropagateCompletion.GetValueOrDefault());
        }
    }
}
