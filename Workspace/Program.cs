using System;
using System.Numerics;
using EmdFlat;

public static class Program
{
    public static unsafe void Main(string[] args)
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

        float[] w1 = [0.4F, 0.3F, 0.2F, 0.1F];
        float[] w2 = [0.5F, 0.3F, 0.2F];

        var s1 = new signature_t<Vector3>(4, f1, w1);
        var s2 = new signature_t<Vector3>(3, f2, w2);

        var emd = new Emd();
        var value = emd.emd(s1, s2, (x, y) => (x - y).Length(), null, null);
        Console.WriteLine(value);
    }
}
