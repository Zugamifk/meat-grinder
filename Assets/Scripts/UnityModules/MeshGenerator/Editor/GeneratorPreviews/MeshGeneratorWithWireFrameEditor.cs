using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public abstract class MeshGeneratorWithWireFrameEditor<TGenerator, TData> : IMeshGeneratorEditor
        where TGenerator : MeshGeneratorWithWireFrame<TData>
        where TData : ScriptableObject
    {
        TGenerator _generator;

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
        }

        public void DrawSceneGUI(Transform rootTransform)
        {
            WireframeDrawer.Draw(_generator.Wireframe);
        }

        public void SetGenerator(IGeometryGenerator generator)
        {
            _generator = (TGenerator)generator;
            _generator.BuildWireframe();
        }
    }
}
