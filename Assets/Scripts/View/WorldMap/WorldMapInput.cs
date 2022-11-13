using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInput : MonoBehaviour
{
    [SerializeField]
    MouseListener _handler;

    private void Start()
    {
        _handler.Id = Game.Model.Input.WorldMapInputHandlerId;
    }
}
