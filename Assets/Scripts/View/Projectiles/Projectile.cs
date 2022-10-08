using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IModelView<IProjectileModel>
{
    Identifiable _identifiable;

    public void InitializeFromModel(IProjectileModel model)
    {
        _identifiable.Id = model.Id;
    }

    private void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    void Update()
    {
        var projectile = Game.Model.Projectiles.GetItem(_identifiable.Id);
        transform.position = projectile.Position;
    }
}
