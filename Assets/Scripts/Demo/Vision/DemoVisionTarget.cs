using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class DemoVisionTarget : MonoBehaviour
    {
        private void Start()
        {
            var id = Guid.NewGuid();
            var i = GetComponent<Identifiable>();
            i.Id = id;
            ViewLookup.Register(id, gameObject);
        }
    }
}
