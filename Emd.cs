using System;

namespace EmdFlat
{
    public sealed unsafe class Emd
    {
        private static readonly int MAX_SIG_SIZE = 100;
        private static readonly int MAX_ITERATIONS = 500;
        private static readonly float INFINITY = (float)1e20;
        private static readonly float EPSILON = (float)1e-6;

        private static readonly int MAX_SIG_SIZE1 = MAX_SIG_SIZE + 1;

        /* GLOBAL VARIABLE DECLARATION */

        /* SIGNATURES SIZES */
        private int _n1;
        private int _n2;

        /* THE COST MATRIX */
        private float[][] _C = CreateJaggedArray<float>(MAX_SIG_SIZE1, MAX_SIG_SIZE1);

        /* THE BASIC VARIABLES VECTOR */
        private node2_t[] managed_X = new node2_t[MAX_SIG_SIZE1 * 2];

        /* VARIABLES TO HANDLE _X EFFICIENTLY */
        private node2_t* _EndX;
        private node2_t* _EnterX;
        private char[][] _IsX = CreateJaggedArray<char>(MAX_SIG_SIZE1, MAX_SIG_SIZE1);
        private node2_t*[] _RowsX = new node2_t*[MAX_SIG_SIZE1];
        private node2_t*[] _ColsX = new node2_t*[MAX_SIG_SIZE1];
        private double _maxW;
        private float _maxC;

        public float emd<feature_t>(
            signature_t<feature_t> Signature1, signature_t<feature_t> Signature2,
            Callback<feature_t> Dist,
            flow_t* Flow, int* FlowSize)
        {
            fixed (node2_t* _X = managed_X)
            {
                int itr;
                double totalCost;
                float w;
                node2_t* XP;
                flow_t* FlowP = null;
                node1_t[] U = new node1_t[MAX_SIG_SIZE1];
                node1_t[] V = new node1_t[MAX_SIG_SIZE1];

                w = init(Signature1, Signature2, Dist);

                if (_n1 > 1 && _n2 > 1)  /* IF _n1 = 1 OR _n2 = 1 THEN WE ARE DONE */
                {
                    for (itr = 1; itr < MAX_ITERATIONS; itr++)
                    {
                        /* FIND BASIC VARIABLES */
                        findBasicVariables(U, V);

                        /* CHECK FOR OPTIMALITY */
                        if (isOptimal(U, V))
                            break;

                        /* IMPROVE SOLUTION */
                        newSol();
                    }

                    if (itr == MAX_ITERATIONS)
                        throw new Exception("emd: Maximum number of iterations has been reached (%d)");
                }

                /* COMPUTE THE TOTAL FLOW */
                totalCost = 0;
                if (Flow != null)
                    FlowP = Flow;
                for (XP = _X; XP < _EndX; XP++)
                {
                    if (XP == _EnterX)  /* _EnterX IS THE EMPTY SLOT */
                        continue;
                    if (XP->i == Signature1.n || XP->j == Signature2.n)  /* DUMMY FEATURE */
                        continue;

                    if (XP->val == 0)  /* ZERO FLOW */
                        continue;

                    totalCost += (double)XP->val * _C[XP->i][XP->j];
                    if (Flow != null)
                    {
                        FlowP->from = XP->i;
                        FlowP->to = XP->j;
                        FlowP->amount = (float)XP->val;
                        FlowP++;
                    }
                }
                if (Flow != null)
                    *FlowSize = (int)(FlowP - Flow);

                /* RETURN THE NORMALIZED COST == EMD */
                return (float)(totalCost / w);
            }
        }

        private static T[][] CreateJaggedArray<T>(int rows, int cols)
        {
            var array = new T[rows][];
            for (var i = 0; i < rows; i++)
            {
                array[i] = new T[cols];
            }
            return array;
        }
    }
}
