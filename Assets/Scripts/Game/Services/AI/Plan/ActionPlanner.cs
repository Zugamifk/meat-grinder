using Codice.Client.Common;
using Codice.Client.Common.TreeGrouper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    internal class ActionPlanner
    {
        HashSet<PlannableAction> _usableActions = new();
        HashSet<PlanNode> _leaves = new();
        HashSet<PlannableAction> _actionSubset = new();
        static Dictionary<string, PlannableAction> _registeredActions = new();

        void Reset()
        {
            _usableActions.Clear();
            _leaves.Clear();
        }

        public PlanModel GetPlan(GameModel game, AIModel ai, State currentState, State goal)
        {
            Reset();

            var plan = new PlanModel();

            foreach (var name in ai.AvailableActions)
            {
                if(!_registeredActions.TryGetValue(name, out PlannableAction action))
                {
                    throw new System.ArgumentException($"No registered action called \'{name}\'!");
                }

                if(action.IsActionUsableForPlan(game, ai))
                {
                    _usableActions.Add(action);
                }
            }

            var start = new PlanNode()
            {
                State = currentState,
            };

            var planExists = FindPlan(start, goal, _usableActions);
            if (!planExists)
            {
                Debug.Log("No plan found!");
                return null;
            }

            PlanNode cheapest = null;
            foreach (var l in _leaves)
            {
                if (cheapest == null || cheapest.Cost > l.Cost)
                {
                    cheapest = l;
                }
            }

            var n = cheapest;
            while(n!=null)
            {
                var action = new ActionModel();
                plan.ActionQueue.Enqueue(action);
            }

            return plan;
        }

        bool FindPlan(PlanNode start, State goal, HashSet<PlannableAction> usableActions)
        {
            var planFound = false;
            foreach(var action in usableActions)
            {
                if(action.Precondition.InState(start.State))
                {
                    var node = GetNextState(start, action);
                    if(node.State.InState(goal))
                    {
                        _leaves.Add(node);
                        planFound = true;
                    } else
                    {
                        PopulateActionSubset(usableActions, action);
                        planFound |= FindPlan(node, goal, _actionSubset);
                    }
                }
            }
            return planFound;
        }

        public PlanNode GetNextState(PlanNode current, PlannableAction action) 
        {
            var node = new PlanNode()
            {
                State = new(),
                Parent = current,
                Action = action,
                Cost = current.Cost + action.Cost
            };
            
            foreach(var s in current.State.Values)
            {
                node.State.Values.Add(s.Key, s.Value);
            }

            foreach(var s in action.Effect.Values)
            {
                node.State.Values[s.Key] = s.Value;
            }

            return node;
        }

        void PopulateActionSubset(HashSet<PlannableAction> actions, PlannableAction toIgnore)
        {
            _actionSubset.Clear();
            foreach(var a in actions)
            {
                if(a!=toIgnore)
                {
                    _actionSubset.Add(a);
                }
            }
        }

    }
}