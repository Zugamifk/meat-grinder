using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Demo.Goap
{
    public class PickUpToolAction : PlannableAction
    {
        public override string Key => "Pick Up Tool";
        public PickUpToolAction()
        {
            AddPrecondition("hasTool", false);
            AddEffect("hasTool", true);
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
