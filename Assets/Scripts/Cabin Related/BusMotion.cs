using System.Collections.Generic;
using UnityEngine;

public class BusMotion : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private List<Transform> MotionAffectedObjects = new List<Transform>();

    [Header("Bobbing Parameters")]
    [SerializeField] float bobSpeed = 3f;
    [SerializeField] float bobAmount = 0.08f;

    [Header("Potholes Parameters")]
    [SerializeField] float shakeAmount = 0.03f;
    [SerializeField] float shakeSpeed = 6f;

    private Vector3 startLocalPos;
    private float bobTimer;
    private float noiseOffset;

    private void Awake()
    {
        startLocalPos = transform.localPosition;
        noiseOffset = Random.Range(0f, 100f);

        foreach (Transform t in MotionAffectedObjects)
        {
            t.parent = this.transform;
        }
    }

    private void Update()
    {
        if (TimeManager.Instance.TimeStop) return;

        bobTimer += Time.deltaTime * bobSpeed;

        // Movimiento vertical suave
        float bob = Mathf.Sin(bobTimer) * bobAmount;

        // Ruido Perlin para baches naturales
        float shakeY = (Mathf.PerlinNoise(Time.time * shakeSpeed, noiseOffset) - 0.5f) * 2f * shakeAmount;
        float shakeX = (Mathf.PerlinNoise(noiseOffset, Time.time * shakeSpeed) - 0.5f) * 2f * shakeAmount * 0.5f;

        Vector3 finalOffset = new Vector3(shakeX, bob + shakeY, 0f);

        transform.localPosition = startLocalPos + finalOffset;
    }
}