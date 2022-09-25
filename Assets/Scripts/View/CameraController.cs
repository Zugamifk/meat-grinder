using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _root;

    private void Update()
    {
        var model = Game.Model.Camera;
        if (model == null) return;

        _root.position = new Vector3(model.Position.x, model.Height, model.Position.y);
        _root.rotation = Quaternion.AngleAxis(model.Angle, Vector3.up);
    }
}
