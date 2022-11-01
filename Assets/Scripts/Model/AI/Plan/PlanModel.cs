using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PlanModel
    {
        public Queue<ActionModel> ActionQueue { get; } = new();
    }
}