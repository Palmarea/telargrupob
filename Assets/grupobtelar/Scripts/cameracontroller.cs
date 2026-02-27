using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController: MonoBehaviour
{
    public float leftLimit = -12f;
    public float rightLimit = 12f;
    public float moveSpeed = 10f;

    [Header("Bobbing (movimiento del bus)")]
    public float bobSpeed = 5f;
    public float bobAmount = 0.2f;

    [Header("Sacudidas aleatorias (baches)")]
    public float shakeAmount = 0.08f;
    public float shakeSpeed = 8f;

    private float defaultY;
    private float bobTimer = 0f;
    private float noiseOffset;

    void Start()
    {
        defaultY = transform.position.y;
        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Movimiento horizontal con mouse
        float mouseX = Mouse.current.position.ReadValue().x / Screen.width;

        float direction = 0f;
        if (mouseX < 0.3f)
            direction = -1f;
        else if (mouseX > 0.7f)
            direction = 1f;

        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        // Bobbing suave
        bobTimer += Time.deltaTime * bobSpeed;
        float bob = Mathf.Sin(bobTimer) * bobAmount;

        // Sacudidas aleatorias (Perlin Noise)
        float shakeY = (Mathf.PerlinNoise(Time.time * shakeSpeed, noiseOffset) - 0.5f) * 2f * shakeAmount;
        float shakeX = (Mathf.PerlinNoise(noiseOffset, Time.time * shakeSpeed) - 0.5f) * 2f * shakeAmount * 0.5f;

        float newY = defaultY + bob + shakeY;
        newX += shakeX;
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}