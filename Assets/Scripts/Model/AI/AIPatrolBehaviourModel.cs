using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolBehaviourModel : IAIBehaviourModel
{
    public List<Vector3> Waypoints { get; } = new();
    public int CurrentWaypointIndex { get; set; } = 0;
}
