using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFrameMeshGeneratorData : ScriptableObject
{
    public Vector2 BaseDimensions;
    [Min(0)]
    public float BorderWidth;
    [Range(0, 1)]
    public float DividerPosition;
    public float DividerWidth;
}
