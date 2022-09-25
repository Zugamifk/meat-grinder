using Demo.Model;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Commands
{
    public class InitializeDemoCommand : CreateModelCommand<DemoModel>
    {
        protected override void OnCreatedModel(GameModel game, DemoModel model)
        {
            model.CurrentMessageIndex = -1;
            Game.Do(new ShowNextMessageCommand());
        }
    }
}
