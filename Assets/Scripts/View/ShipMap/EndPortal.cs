using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;

public class EndPortal
    : MonoBehaviour
{
    [SerializeField]
    MeshFilter _meshFilter;

    private void Start()
    {
        var gen = new EndPortalMeshGenerator();
        var res  = gen.Generate();
        _meshFilter.mesh = res.Mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Game.Do(new EnemyReachEndCommand(enemy.Id));
        }
    }
}
