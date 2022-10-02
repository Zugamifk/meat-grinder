using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour, IView<IBuildingModel>
{
    public void InitializeFromModel(IBuildingModel model)
    {
        Debug.Log("Spawned " + model.Key);
    }
}
