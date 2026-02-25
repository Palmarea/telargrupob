using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float leftLimit = -12f;
    public float rightLimit = 12f;
    public float moveSpeed = 10f;

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        float mouseX = mousePos.x / Screen.width;

        float direction = 0f;
        if (mouseX < 0.3f)
            direction = -1f;
        else if (mouseX > 0.7f)
            direction = 1f;

        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}