using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class ActionModel
    {
        public Dictionary<string, bool> Effectes { get; } = new();
        public float Weight { get; set; }
    }
}