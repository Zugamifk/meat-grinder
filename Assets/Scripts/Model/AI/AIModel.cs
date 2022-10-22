using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModel : IIdentifiable
{
    public Guid Id { get; } = Guid.NewGuid();
    public AIBehaviourModel Behaviour { get; set; } = new();
    public Guid AgentId { get; set; }
}
