using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public abstract class MeshGeneratorEditorWithWireFrame<TGenerator, TWireframeGenerator, TData> : IMeshGeneratorEditor
        where TGenerator : MeshGeneratorWithData<TData>
        where TWireframeGenerator : WireframeGenerator<TData>, new()
        where TData : ScriptableObject
    {
        protected TGenerator _generator;

        protected Wireframe _wireframe = new();
        protected TWireframeGenerator _wireframeGenerator = new();
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
                OnPropertiesChanged();
                RebuildWireframe();
            }

            DrawInspectorFields();
        }

        protected virtual void DrawInspectorFields()
        {

        }

        protected virtual void OnPropertiesChanged() { }

        public void DrawSceneGUI(Transform rootTransform)
        {
            if (_wireframe != null)
            {
                WireframeDrawer.Draw(_wireframe);
            }
        }

        public void RebuildWireframe()
        {
            _wireframe.Clear();
            _wireframeGenerator.Generate(_wireframe, _data);
        }


        public void SetGenerator(IGeometryGenerator generator)
        {
            _generator = (TGenerator)generator;
            OnSetGenerator();
            RebuildWireframe();
        }

        protected virtual void OnSetGenerator()
        {

        }
    }
}
