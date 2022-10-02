using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelView<TModel>
{
    void InitializeFromModel(TModel model);
}
