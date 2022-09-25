using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject, IRegisteredData
{
    public Color GrassColor;
    public Color PathColor;
    public float RoadWidth;
    public Vector2Int Dimensions;
    public float TileStepHeight = .4f;
}
