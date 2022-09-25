using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Demo.ViewModel;
using Demo.Commands;

namespace Demo.View
{
    public class DemoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _messageText;

        private void Update()
        {
            var model = Game.Model.GetModel<IDemoModel>();
            if (model != null)
            {
                _messageText.text = model.Message;
            }
        }

        public void ShowNextMessage()
        {
            Game.Do(new ShowNextMessageCommand());
        }
    }
}
