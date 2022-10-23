using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModel : IIdentifiable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public AIBehaviourModel Behaviour { get; set; } = new();
}
