using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using AI;

namespace Demo.Goap
{
    public class GOAPTestModel : IRegisteredModel
    {
        public bool IsToolAvailable;
        public bool IsTreeAvailable;
        public HashSet<string> AvailableActions = new();
    }
}
