# EmdFlat

This is a C# port of the well-known [Earth Mover's Distance](https://ai.stanford.edu/~rubner/emd/default.htm) implementation by Rubner.

> [!NOTE]
> This library was created as an EMD implementation for [NumFlat](https://github.com/sinshu/numflat),
> a general purpose numerical computing library written in pure C#.
> If you're interested in numerical computing with C#, please check out [NumFlat](https://github.com/sinshu/numflat).



## Usage

The following are C# reproductions of two example cases provided by the original author.

### Example 1
```cs
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

var value = emd.emd(s1, s2, (x, y) => (x - y).Length(), null, null);
Console.WriteLine($"emd={value}");
```

### Example 2

```cs
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

fixed (flow_t* p = flow)
{
    var value = emd.emd(s1, s2, (x, y) => cost[x][y], p, &flowSize);
    Console.WriteLine($"emd={value}");
}

Console.WriteLine();
Console.WriteLine($"flow:");
Console.WriteLine("from\tto\tamount");
for (var i = 0; i < flowSize; i++)
{
    Console.WriteLine($"{flow[i].from}\t{flow[i].to}\t{flow[i].amount}");
}
```
