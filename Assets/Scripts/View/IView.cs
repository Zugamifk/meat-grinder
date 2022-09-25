using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView<TModel>
{
    void InitializeFromModel(TModel model);
}
