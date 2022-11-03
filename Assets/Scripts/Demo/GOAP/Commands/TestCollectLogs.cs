using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;
using Codice.Client.Common.FsNodeReaders;

namespace Demo.Goap
{
    public class TestCollectLogs : ICommand
    {
        public void Execute(GameModel model)
        {
            var goap = model.GetModel<GOAPTestModel>();
            var id = Guid.NewGuid();
            new CreateAI(id).Execute(model);
            var service = new AIService();
            
            var ai = model.AI.GetItem(id);
            ai.AvailableActions = goap.AvailableActions;
            var start = new State();
            start.AddValue("hasTool", false);
            start.AddValue("hasLogs", false);
            start.AddValue("logsCollected", false);
            var goal = new State();
            goal.AddValue("logsCollected", true);
            service.Plan(model, ai, start, goal);
            Debug.Log(ai.Plan);
        }
    }
}
