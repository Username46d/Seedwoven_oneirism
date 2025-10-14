using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float camSpeed = 10f;
    private float Border = 10f;
    private bool edgeBorder = true;

    private Camera mainCamera;

    private float zoomSpeed = 3f;
    private float minZoom = 2f;
    private float maxZoom = 10f;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CameraMovement();
        ZoomCamera();
    }

    void CameraMovement()
    {
        Vector3 moveDir = NewPosition();
        transform.position += moveDir * camSpeed * Time.deltaTime;
    }
    Vector3 NewPosition()
    {
        Vector3 direction = new Vector3();

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        return direction;
    }
    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }
}
