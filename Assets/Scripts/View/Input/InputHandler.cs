using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    string _handlerKey;

    public string HandlerKey => _handlerKey;
    public Guid Id;

    private void Awake()
    {
        var ident = GetComponent<Identifiable>();
        if(ident!=null)
        {
            Id = ident.Id;
        }
    }
}
