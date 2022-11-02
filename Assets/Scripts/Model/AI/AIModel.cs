using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace AI
{
    public class AIModel : IIdentifiable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IAIBehaviourModel Behaviour { get; set; }
        public PlanModel Plan { get; set; }
        public IEnumerable<string> AvailableActions { get; set; }
    }
}