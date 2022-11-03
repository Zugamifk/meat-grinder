using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Goap
{
    public class ChopTreeAction : PlannableAction
    {
        public override string Key => "Chop Tree";
        public ChopTreeAction()
        {
            AddPrecondition("hasTool", true);
            AddPrecondition("hasLogs", false);
            AddEffect("hasLogs", true);
        }

        public override IAIBehaviourModel GetBehaviourModel(GameModel game, AIModel ai)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsActionUsableForPlan(GameModel game, AIModel ai)
        {
            var goap = game.GetModel<GOAPTestModel>();
            return goap.IsToolAvailable;
        }
    }
}
