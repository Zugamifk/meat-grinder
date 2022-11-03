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
        HashSet<PlanNode> _leaves = new();
        static Dictionary<string, PlannableAction> _registeredActions = new();

        void Reset()
        {
            _leaves.Clear();
        }

        internal void RegisterPlannableAction(PlannableAction action)
        {
            _registeredActions.Add(action.Key, action);
        }

        public PlanModel GetPlan(GameModel game, AIModel ai, State currentState, State goal)
        {
            Reset();

            var plan = new PlanModel();
            var usableActions = new HashSet<PlannableAction>();
            foreach (var name in ai.AvailableActions)
            {
                if(!_registeredActions.TryGetValue(name, out PlannableAction action))
                {
                    throw new System.ArgumentException($"No registered action called \'{name}\'!");
                }

                if(action.IsActionUsableForPlan(game, ai))
                {
                    usableActions.Add(action);
                }
            }

            var start = new PlanNode()
            {
                State = currentState,
            };

            var planExists = FindPlan(start, goal, usableActions);
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

            var actions = new List<IAIBehaviourModel>();
            for (var n = cheapest; n != start; n = n.Parent)
            {
                var behaviour = n.Action.GetBehaviourModel(game, ai);
                actions.Insert(0, behaviour);
            }
            plan.ActionQueue = new Queue<IAIBehaviourModel>(actions);

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
                    if(goal.InState(node.State))
                    {
                        _leaves.Add(node);
                        planFound = true;
                    } else
                    {
                        var actionSubset = PopulateActionSubset(usableActions, action);
                        planFound |= FindPlan(node, goal, actionSubset);
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
                Cost = current.Cost + action.Cost,
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

        HashSet<PlannableAction> PopulateActionSubset(HashSet<PlannableAction> actions, PlannableAction toIgnore)
        {
            var actionSubset = new HashSet<PlannableAction>();
            foreach(var a in actions)
            {
                if(a!=toIgnore)
                {
                    actionSubset.Add(a);
                }
            }
            return actionSubset;
        }

    }
}