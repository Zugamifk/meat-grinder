using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IModelView<IProjectileModel>
{
    [SerializeField]
    TrailRenderer _trailRenderer;

    Identifiable _identifiable;

    Enemy _targetEnemy;

    public IProjectileModel GetModel() => Game.Model.Projectiles.GetItem(_identifiable.Id);

    public void InitializeFromModel(IProjectileModel model)
    {
        _identifiable.Id = model.Id;
        transform.position = model.Position;

        var go = ViewLookup.Get(model.TargetEnemyId);
        _targetEnemy = go.GetComponent<Enemy>();
    }

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    void Update()
    {
        var projectile = Game.Model.Projectiles.GetItem(_identifiable.Id);
        if(projectile == null) return;

        transform.position = projectile.Position + _targetEnemy.TargetOffset;
    }
}
