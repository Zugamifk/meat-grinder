using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour, IModelView<IBuildingModel>
{
    public void InitializeFromModel(IBuildingModel model)
    {
        Debug.Log("Spawned " + model.Key);
    }
}
