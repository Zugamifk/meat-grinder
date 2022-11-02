using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class PlannableAction
    {
        internal readonly State Precondition = new();
        internal readonly State Effect = new();
        protected internal float Cost { get; }
        public abstract string Name { get; }
        public abstract bool IsActionUsableForPlan(GameModel game, AIModel ai);

        protected void AddPrecondition(string key, bool value) => Precondition.Values.Add(key, value);
        protected void AddEffect(string key, bool value) => Effect.Values.Add(key, value);
    }
}