using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    Camera _raycastCamera;

    Vector2 _lastMousePosition;

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
    }

    void HandleKeyboardInput()
    {
        var move = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x = 1;
        }

        if (move != Vector2.zero)
        {
            Game.Do(new MoveCameraCommand(move));
        }
    }

    void HandleMouseInput()
    {
        if(HandleWorldInput())
        {
            return;
        }

        HandleRotateCamera();
    }

    bool HandleWorldInput()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return false;
        }

        Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return false;
        }

        var handler = hit.collider.GetComponent<InputHandler>();
        if (handler == null)
        {
            return false;
        }

        Game.Do(new ClickedWorldObjectCommand(handler.Id));

        return true;
    }

    void HandleRotateCamera()
    {
        var position = (Vector2)Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            var delta = position - _lastMousePosition;
            Game.Do(new RotateCameraCommand(delta.x));
        }

        _lastMousePosition = position;
    }
}
