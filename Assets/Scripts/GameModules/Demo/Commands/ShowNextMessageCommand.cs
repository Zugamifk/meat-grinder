using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.Model;
using Demo.Data;

namespace Demo.Commands
{
    public class ShowNextMessageCommand : ICommand
    {
        public void Execute(GameModel model)
        {
            var demo = model.GetModel<DemoModel>();
            var messages = DataService.GetData<DemoMessageData>();
            if (demo.CurrentMessageIndex < messages.Messages.Length - 1)
            {
                demo.CurrentMessageIndex++;
                demo.Message = messages.Messages[demo.CurrentMessageIndex];
            }
        }
    }
}
