/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        zCoord = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            rb.MovePosition(GetMouseWorldPos() + offset);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}



*/
using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Touchscreen.current == null) return;

        // Check for touch input
        var touch = Touchscreen.current.primaryTouch;
        if (touch.press.isPressed)
        {
            if (!isDragging)
            {
                OnTouchDown(touch.position.ReadValue());
            }
            else
            {
                OnTouchMove(touch.position.ReadValue());
            }
        }
        else if (isDragging)
        {
            OnTouchUp();
        }
    }

    private void OnTouchDown(Vector2 touchPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            zCoord = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetTouchWorldPos(touchPosition);
            isDragging = true;
        }
    }

    private void OnTouchMove(Vector2 touchPosition)
    {
        rb.MovePosition(GetTouchWorldPos(touchPosition) + offset);
    }

    private void OnTouchUp()
    {
        isDragging = false;
    }

    private Vector3 GetTouchWorldPos(Vector2 touchPosition)
    {
        Vector3 touchPoint = new Vector3(touchPosition.x, touchPosition.y, zCoord);
        return mainCamera.ScreenToWorldPoint(touchPoint);
    }
}
