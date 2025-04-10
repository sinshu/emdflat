using System;
using System.Numerics;
using EmdFlat;

namespace EmdFlatTest
{
    [TestClass]
    public sealed class Example1
    {
        [TestMethod]
        public unsafe void Run()
        {
            Vector3[] f1 =
            [
                new(100, 40, 22),
                new(211, 20, 2),
                new(32, 190, 150),
                new(2, 100, 100),
            ];
            Vector3[] f2 =
            [
                new(0, 0, 0),
                new(50, 100, 80),
                new(255, 255, 255),
            ];

            double[] w1 = [0.4, 0.3, 0.2, 0.1];
            double[] w2 = [0.5, 0.3, 0.2];

            var s1 = new signature_t<Vector3>(4, f1, w1);
            var s2 = new signature_t<Vector3>(3, f2, w2);

            var emd = new Emd();
            var actual = emd.emd(s1, s2, (x, y) => (x - y).Length(), null, null);

            var expected = 160.542770;
            Assert.AreEqual(expected, actual, 1.0E-5);
        }
    }
}
