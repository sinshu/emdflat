using System;
using System.Numerics;
using EmdFlat;

public static class Program
{
    public static unsafe void Main(string[] args)
    {
        double[][] cost =
        [
            [3, 5, 2],
            [0, 2, 5],
            [1, 1, 3],
            [8, 4, 3],
            [7, 6, 5],
        ];

        int[] f1 = [0, 1, 2, 3, 4];
        int[] f2 = [0, 1, 2];
        double[] w1 = [0.4, 0.2, 0.2, 0.1, 0.1];
        double[] w2 = [0.6, 0.2, 0.1];
        var s1 = new signature_t<int>(5, f1, w1);
        var s2 = new signature_t<int>(3, f2, w2);

        flow_t[] flow = new flow_t[7];
        int flowSize = 0;

        var emd = new Emd();
        double value;
        fixed (flow_t* p = flow)
        {
            value = emd.emd(s1, s2, (x, y) => cost[x][y], p, &flowSize);
        }
        Console.WriteLine(value);
        for (var i = 0; i < flowSize; i++)
        {
            Console.WriteLine($"{flow[i].from} => {flow[i].to} ({flow[i].amount})");
        }
    }
}
