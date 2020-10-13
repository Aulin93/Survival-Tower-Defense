using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera MainCamera { get; private set; }

    private float minRotation = -90f;
    private float maxRotation = 90f;
    [SerializeField] private float mouseSensitivity = 1f;
    private float speed = 10f;
    private Vector2 currentRotation;

    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
        Vector3 initialRotation = transform.rotation.eulerAngles;
        currentRotation = new Vector2(initialRotation.x, initialRotation.y);
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            currentRotation += new Vector2(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity);
            currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.Q))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            input.y -= 1;
        }
        transform.Translate(input * speed * Time.deltaTime);
    }
}
