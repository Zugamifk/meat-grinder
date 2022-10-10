using MeshGenerator.Wireframe;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public abstract class MeshGeneratorEditorWithWireFrame<TGenerator, TData> : IMeshGeneratorEditor
        where TGenerator : MeshGeneratorWithData<TData>
        where TData : ScriptableObject
    {
        protected Frame _wireframe;

        protected TGenerator _generator;
        protected TData _data => _generator.Data;

        public void DrawInspectorGUI()
        {
            var d = _generator.Data;

            EditorGUI.BeginChangeCheck();

            var so = new SerializedObject(d);
            var iter = so.GetIterator();
            if (iter.NextVisible(true))
            {
                iter.NextVisible(false);
                do
                {
                    EditorGUILayout.PropertyField(iter, true);
                }
                while (iter.NextVisible(false));
            }

            if (EditorGUI.EndChangeCheck())
            {
                so.ApplyModifiedProperties();
                EditorUtility.SetDirty(d);
            }

            DrawInspectorFields();
        }

        protected virtual void DrawInspectorFields()
        {

        }

        public void DrawSceneGUI(Transform rootTransform)
        {
            if (_wireframe != null)
            {
                WireframeDrawer.Draw(_wireframe);
            }
        }

        public abstract void BuildWireframe();

        public void SetGenerator(IGeometryGenerator generator)
        {
            _generator = (TGenerator)generator;
            OnSetGenerator();
            BuildWireframe();
        }

        protected virtual void OnSetGenerator()
        {

        }
    }
}
