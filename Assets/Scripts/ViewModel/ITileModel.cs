using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileModel
{
    ITileStructure Structure { get; }
    int Height { get; }
    bool HasPath { get; }
}
