using System;

namespace EmdFlat
{
    public sealed class flow_t
    {
        public int from;      /* Feature number in signature 1 */
        public int to;        /* Feature number in signature 2 */
        public float amount;  /* Amount of flow from "from" to "to" */
    }
}
