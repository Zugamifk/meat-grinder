using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Goap
{
    public class GOAPTest : MonoBehaviour
    {
        private void Start()
        {
            Game.Do(new CreateTestModelCommand());
            Game.Do(new TestCollectLogs());
        }
    }
}
