using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Demo.Goap
{
    public class PickUpToolAction : PlannableAction
    {
        public PickUpToolAction()
        {
            AddPrecondition("hasTool", false);
            AddEffect("hasTool", true);
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
