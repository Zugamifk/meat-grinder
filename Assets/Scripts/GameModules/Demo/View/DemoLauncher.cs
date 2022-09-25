using Demo.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.View
{
    public class DemoLauncher : MonoBehaviour
    {
        void Start()
        {
            Game.Do(new InitializeDemoCommand());
        }
    }
}
