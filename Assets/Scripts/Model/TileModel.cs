using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel : ITileModel
{
    public ITileStructure Structure { get; set; }
    public bool HasPath { get; set; }
    public int Height { get; set; }
}
