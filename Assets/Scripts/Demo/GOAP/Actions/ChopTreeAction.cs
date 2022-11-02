using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Goap
{
    public class ChopTreeAction : PlannableAction
    {
        public ChopTreeAction()
        {
            AddPrecondition("hasTool", true);
            AddPrecondition("hasLogs", false);
            AddEffect("hasLogs", true);
        }

        public override string Name => "Chop Tree";

        public override bool IsActionUsableForPlan(GameModel game, AIModel ai)
        {
            var goap = game.GetModel<GOAPTestModel>();
            return goap.IsToolAvailable;
        }
    }
}
