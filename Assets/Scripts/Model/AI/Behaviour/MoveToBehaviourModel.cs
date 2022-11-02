using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class MoveToBehaviourModel : IAIBehaviourModel
    {
        public Vector3 Destination;

        public string Key => "Move";
    }
}