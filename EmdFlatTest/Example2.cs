using System;
using System.Numerics;
using EmdFlat;

namespace EmdFlatTest
{
    [TestClass]
    public sealed class Example2
    {
        [TestMethod]
        public unsafe void Run()
        {
            float[][] cost =
            [
                [3, 5, 2],
                [0, 2, 5],
                [1, 1, 3],
                [8, 4, 3],
                [7, 6, 5],
            ];

            int[] f1 = [0, 1, 2, 3, 4];
            int[] f2 = [0, 1, 2];
            float[] w1 = [0.4F, 0.2F, 0.2F, 0.1F, 0.1F];
            float[] w2 = [0.6F, 0.2F, 0.1F];
            var s1 = new signature_t<int>(5, f1, w1);
            var s2 = new signature_t<int>(3, f2, w2);

            flow_t[] flow = new flow_t[7];
            int flowSize = 0;

            var emd = new Emd();
            float actualEmd;
            fixed (flow_t* p = flow)
            {
                actualEmd = emd.emd(s1, s2, (x, y) => cost[x][y], p, &flowSize);
            }

            var expectedEmd = 1.888889;
            Assert.AreEqual(expectedEmd, actualEmd, 1.0E-5);

            Assert.AreEqual(7, flowSize);

            (int From, int To, float Amount)[] expectedFlow =
            [
                (1, 0, 0.200000F),
                (0, 0, 0.300000F),
                (2, 0, 0.100000F),
                (3, 1, 0.100000F),
                (2, 1, 0.100000F),
                (4, 2, 0.000000F),
                (0, 2, 0.100000F),
            ];

            for (var i = 0; i < flowSize; i++)
            {
                var expected = expectedFlow[i];
                var actual = flow[i];
                Assert.AreEqual(expected.From, actual.from);
                Assert.AreEqual(expected.To, actual.to);
                Assert.AreEqual(expected.Amount, actual.amount, 1.0E-5);
            }
        }
    }
}
