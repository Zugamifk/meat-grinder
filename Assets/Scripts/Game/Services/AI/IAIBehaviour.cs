using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IAIBehaviour
    {
        void Update(GameModel model, AIModel ai);

    }
}