using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class PlanModel
    {
        public Queue<ActionModel> ActionQueue { get; } = new();

        public override string ToString()
        {
            return string.Join(" -> ", ActionQueue.Select(a => a.ActionName));
        }
    }
}