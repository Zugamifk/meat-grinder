using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class UIDemo : MonoBehaviour
    {
        private void Start()
        {
            Scenes.LoadShipInterior();
            Scenes.LoadNavigation();
        }
    }
}
