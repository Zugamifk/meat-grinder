using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IModelView<IProjectileModel>
{
    [SerializeField]
    TrailRenderer _trailRenderer;

    Identifiable _identifiable;

    public void InitializeFromModel(IProjectileModel model)
    {
        _identifiable.Id = model.Id;
        transform.position = model.Position;
    }

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    void Update()
    {
        var projectile = Game.Model.Projectiles.GetItem(_identifiable.Id);
        if(projectile == null) return;

        transform.position = projectile.Position;
        Game.Do(new UpdateProjectileCommand(_identifiable.Id));
    }
}
