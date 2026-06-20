using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 3f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, 5f, 60f);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + Vector3.up * height + offset;
        transform.LookAt(target.position + Vector3.up * height);
    }
}