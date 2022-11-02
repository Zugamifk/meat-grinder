using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolBehaviourModel : IAIBehaviourModel
    {
        public List<Vector3> Waypoints { get; } = new();
        public int CurrentWaypointIndex { get; set; } = 0;

        public string Key => "Patrol";
    }
}