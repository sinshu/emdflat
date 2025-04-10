using System;

namespace EmdFlat
{
    public sealed class signature_t<feature_t>
    {
        public int n;                 /* Number of features in the signature */
        public feature_t[] Features;  /* Pointer to the features vector */
        public double[] Weights;      /* Pointer to the weights of the features */

        public signature_t(int n, feature_t[] Features, double[] Weights)
        {
            this.n = n;
            this.Features = Features;
            this.Weights = Weights;
        }
    }
}
