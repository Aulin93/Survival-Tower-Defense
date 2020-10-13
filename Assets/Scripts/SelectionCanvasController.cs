using UnityEngine;

public class SelectionCanvasController : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(CameraController.MainCamera.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
