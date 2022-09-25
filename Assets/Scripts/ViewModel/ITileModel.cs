using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileModel
{
    ITileStructure Structure { get; }
    bool HasPath { get; }
    int Height { get; }
}
