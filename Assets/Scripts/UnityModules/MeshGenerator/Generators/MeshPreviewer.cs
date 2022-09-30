using UnityEngine;

namespace MeshGenerator
{
    public class MeshPreviewer : MonoBehaviour
    {
        [SerializeField]
        Mesh _mesh;
        [SerializeField]
        string _meshType;
        [SerializeField]
        Transform _generatorTransform;

        public IGeometryGenerator CurrentGenerator { get; private set; }

        public void SetMesh(Mesh mesh)
        {
            var mf = GetComponent<MeshFilter>();
            mf.mesh = mesh;
            _mesh = mesh;
        }

        public void Clear()
        {
            _mesh = null;
            var mf = GetComponent<MeshFilter>();
            mf.mesh = null;
        }
    }
}
