using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class VisionTarget : MonoBehaviour
{
    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    private void Start()
    {
        _identifiable = GetComponent<Identifiable>();
    }
}
