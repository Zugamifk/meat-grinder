using MeshGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MeshGeneratorUser, IModelView<IEnemyModel>
{
    [SerializeField]
    MeshFilter _meshFilter;

    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    public void InitializeFromModel(IEnemyModel model)
    {
        _identifiable.Id = model.Id;
    }

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    private void Start()
    {
        AssignMesh<EnemyMeshGenerator>(_meshFilter);
    }

    private void Update()
    {
        var enemy = Game.Model.SpawnedEnemies.GetItem(_identifiable.Id);
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        var position = enemy.Position;
        position.y = Test.Instance.Map.GetTile(new Vector2Int((int)(position.x + .5f), (int)(position.z + .5f))).SurfaceY;
        transform.position = position;
        Game.Do(new UpdateEnemyMovementCommand(_identifiable.Id));
    }
}
