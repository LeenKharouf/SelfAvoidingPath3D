using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;


public class UserControlledCamera1 : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Speed of rotation
    public float zoomSpeed = 10.0f; // Speed of zooming
    public float minZoomDistance = 5.0f; // Minimum zoom distance
    public float maxZoomDistance = 50.0f; // Maximum zoom distance

    private Vector3 target; // Center of the grid

    void Start()
    {
        // Assuming gridSize is accessible here, adjust accordingly if it's not
        target = new Vector3(5, 5, 5);
    }

    void Update()
    {
        // Rotate around the grid center
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(target, Vector3.up, rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        // Zoom in and out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 direction = transform.position - target;
            float distance = direction.magnitude;
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);
            transform.position = target + direction.normalized * distance;
        }
    }
}

