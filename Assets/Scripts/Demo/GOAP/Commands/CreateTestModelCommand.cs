using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Goap
{
    public class CreateTestModelCommand : CreateModelCommand<GOAPTestModel>
    {
        protected override void OnCreatedModel(GameModel game, GOAPTestModel model)
        {
            model.AvailableActions.Add("Pick Up Tool");
            model.AvailableActions.Add("Chop Tree");
            model.AvailableActions.Add("Drop Off Logs");

            AIService.RegisterAction<GOAPTestBehaviourModel>(null, new PickUpToolAction());
            AIService.RegisterAction<GOAPTestBehaviourModel>(null, new ChopTreeAction());
            AIService.RegisterAction<GOAPTestBehaviourModel>(null, new DropOffLogsAction());
        }
    }
}
