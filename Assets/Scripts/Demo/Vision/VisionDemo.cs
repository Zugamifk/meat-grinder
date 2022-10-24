using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDemo : MonoBehaviour
{
    [SerializeField]
    VisionRadius _radius;

    void Start()
    {
        var id = Guid.NewGuid();
        Game.Do(new CreateAICommand(id));
        _radius.Id = id;
    }
}
