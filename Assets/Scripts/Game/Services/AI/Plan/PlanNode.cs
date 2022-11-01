using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    internal class PlanNode
    {
        public State State = null;
        public PlannableAction Action = null;
        public PlanNode Parent = null;
        public float Cost = 0;
    }
}