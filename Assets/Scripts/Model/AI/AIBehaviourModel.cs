using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourModel : IKeyHolder
{
    public List<Vector3> Waypoints { get; } = new();
    public int CurrentWaypointIndex { get; set; } = 0;
    public string Key { get; set; }
}
