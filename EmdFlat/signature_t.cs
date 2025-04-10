using System;

namespace EmdFlat
{
    public sealed class signature_t<feature_t>
    {
        public int n;               /* Number of features in the signature */
        public feature_t[] Features;  /* Pointer to the features vector */
        public float[] Weights;     /* Pointer to the weights of the features */
    }
}
