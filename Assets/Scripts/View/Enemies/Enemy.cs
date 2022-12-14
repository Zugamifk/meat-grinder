using MeshGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MeshGeneratorUser, IModelView<IEnemyModel>
{
    [SerializeField]
    Transform _targetOffset;
    [SerializeField]
    MeshFilter _meshFilter;
    [SerializeField]
    Transform _viewRoot;
    [SerializeField]
    ParticleSystem _bloodSplatter;

    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;
    public Vector3 TargetOffset { get; private set; }

    public IEnemyModel GetModel() => Game.Model.Enemies.GetItem(Id);

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
        var enemy = Game.Model.Enemies.GetItem(_identifiable.Id);
        if (enemy == null)
        {
            return;
        }

        var position = enemy.Position;
        //var height = ShipMapTest.Instance.Map.GetTile(new Vector2Int((int)(position.x + .5f), (int)(position.z + .5f))).SurfaceY;
        //position.y = height;
        transform.position = position;

        var targetOffset = _targetOffset.localPosition;
        //targetOffset.y += height;
        TargetOffset = targetOffset;
    }

    public void OnKilled()
    {
        var go = new GameObject("Corpse");
        var collider = go.AddComponent<BoxCollider>();
        var b = _meshFilter.mesh.bounds;
        collider.size = b.size;
        collider.center = b.center;
        var rb = go.AddComponent<Rigidbody>();
        go.transform.position = _viewRoot.parent.position;
        _viewRoot.SetParent(go.transform);
        rb.AddForce(UnityEngine.Random.onUnitSphere * 10, ForceMode.VelocityChange);
        rb.angularVelocity = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-10, 10);
        _bloodSplatter.Play();
        _bloodSplatter.transform.localRotation = UnityEngine.Random.rotationUniform;
    }
}
