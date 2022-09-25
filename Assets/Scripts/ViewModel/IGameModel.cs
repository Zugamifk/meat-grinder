using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
    IMapModel Map { get; }
    IWaveModel CurrentWave { get; }
    IIdentifiableLookup<IEnemyModel> SpawnedEnemies { get; }
    IIdentifiableLookup<IEnemySpawnModel> Spawns { get; }
    ICameraModel Camera { get; }
}