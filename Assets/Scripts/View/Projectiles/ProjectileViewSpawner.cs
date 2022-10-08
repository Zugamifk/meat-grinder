using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileViewSpawner : RegisteredPrefabViewSpawner<IProjectileModel, Projectile>
{
    protected override IEnumerable<IProjectileModel> AllModels() => Game.Model.Projectiles.AllItems;

    protected override IProjectileModel GetModel(Guid id) => Game.Model.Projectiles.GetItem(id);
}
