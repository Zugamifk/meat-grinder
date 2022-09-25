using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject, IKeyHolder
{
    [SerializeField] string _key;
    public string Key => _key;
    public float MoveSpeed;
}
