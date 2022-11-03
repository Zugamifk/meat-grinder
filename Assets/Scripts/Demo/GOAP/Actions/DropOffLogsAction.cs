using AI;
using Demo.Goap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class DropOffLogsAction : PlannableAction
    {
        public override string Key => "Drop Off Logs";

        public DropOffLogsAction()
        {
            AddPrecondition("hasLogs", true);
            AddEffect("hasLogs", false);
            AddEffect("logsCollected", true);
        }

        public override IAIBehaviourModel GetBehaviourModel(GameModel game, AIModel ai)
        {
            return new GOAPTestBehaviourModel() { Key = Key };
        }

        public override bool IsActionUsableForPlan(GameModel game, AIModel ai)
        {
            var goap = game.GetModel<GOAPTestModel>();
            return goap.IsToolAvailable;
        }
    }
}
