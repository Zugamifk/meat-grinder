using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.ViewModel;

namespace Demo.Model
{
    public class DemoModel : IDemoModel
    {
        public int CurrentMessageIndex { get; set; }
        public string Message { get; set; }
    }
}
