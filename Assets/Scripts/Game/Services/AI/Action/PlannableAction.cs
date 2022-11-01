using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class PlannableAction
    {
        internal State Precondition;
        internal State Effect;
        internal float Cost;
        public abstract bool IsActionUsable(GameModel game, AIModel ai);
    }
}