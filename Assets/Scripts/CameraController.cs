using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Настройки")]
    public float camSpeed = 10f;
    public float zoomSpeed = 3f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float dragSpeed = 10f;

    private float Border = 10f;
    private bool edgeBorder = true;

    private Vector3 drag;
    private bool isDragging = false;

    private Camera mainCamera;

    private
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CameraMovement();
        ZoomCamera();
        if (Input.GetMouseButtonDown(2))
        {
            isDragging = true;
            drag = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
        }
        if (isDragging && Input.GetMouseButton(2))
        {

            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition - drag);
            Vector3 move = new Vector3(position.x * dragSpeed, position.y * dragSpeed, 0);
            transform.Translate(-move, Space.World);
            drag = Input.mousePosition;
        }
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
