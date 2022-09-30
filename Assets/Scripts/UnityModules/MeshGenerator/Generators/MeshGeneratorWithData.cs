using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Wireframe;

namespace MeshGenerator
{
    public abstract class MeshGeneratorWithData<TData> : MeshGenerator
        where TData : ScriptableObject
    {
        static TData _data;
        public TData Data
        {
            get
            {
                if (_data == null)
                {
                    _data = LoadData();
                }
                return _data;
            }
        }
        protected abstract TData LoadData();

    }
}
