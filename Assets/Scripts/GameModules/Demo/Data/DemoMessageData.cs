using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Data
{
    [CreateAssetMenu(menuName = "Data/Demo/Messages")]
    public class DemoMessageData : ScriptableObject, IRegisteredData
    {
        public string[] Messages;
    }
}
