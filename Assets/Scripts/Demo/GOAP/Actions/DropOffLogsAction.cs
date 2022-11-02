using AI;
using Demo.Goap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class DropOffLogsAction : PlannableAction
    {
        public DropOffLogsAction()
        {
            AddPrecondition("hasLogs", true);
            AddEffect("hasLogs", false);
            AddEffect("logsCollected", true);
        }

        public override string Name => "Drop Off Logs";

        public override bool IsActionUsableForPlan(GameModel game, AIModel ai)
        {
            var goap = game.GetModel<GOAPTestModel>();
            return goap.IsToolAvailable;
        }
    }
}
