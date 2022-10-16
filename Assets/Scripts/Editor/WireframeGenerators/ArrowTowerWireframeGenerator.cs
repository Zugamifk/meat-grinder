using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public class ArrowTowerWireframeGenerator : WireframeGenerator<ArrowTowerMeshGeneratorData>
    {
        protected override void BuildWireframe(Wireframe wireframe, ArrowTowerMeshGeneratorData data)
        {
            void AddRing(float t)
            {
                wireframe.Rings.Add(new Ring()
                {
                    Center = new DynamicPoint(() => new Vector3(0, t * data.Height, 0)),
                    Normal = () => Vector3.up,
                    Radius = () => Mathf.Lerp(data.RadiusMinMax.x, data.RadiusMinMax.y, data.TowerCurve.Evaluate(t))
                });
            }

            for (float t = 0; t < 1; t += 0.25f / data.Height)
            {
                AddRing(t);
            }
            AddRing(1);
        }
    }
}
