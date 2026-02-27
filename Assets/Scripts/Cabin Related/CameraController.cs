using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float LeftDeadZone = 0.3f;
    [SerializeField] private float RightDeadZone = 0.7f;
    [SerializeField] private float LeftBound = -3f;
    [SerializeField] private float RightBound = 7f;
    [SerializeField] private float MoveSpeed = 10f;

    private bool activeState = true;
    private bool suscribed = false;

    private void Start()
    {
        if (!suscribed)
        {
            ClickDetector.Instance.OnOcuppiedStateChanged += CheckForActivationStatus;
        }
    }

    void Update()
    {
        if (!activeState) return;
        
        Vector2 mousePos = InputManager.Instance.GetRawMousePosition();
        float mouseX = mousePos.x / Screen.width;
        float direction = mouseX < LeftDeadZone ? -1 : mouseX > RightDeadZone ? 1 : 0;

        float newX = transform.position.x + direction * MoveSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, LeftBound, RightBound);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void CheckForActivationStatus(bool ocuppiedStatus)
    {
        activeState = !ocuppiedStatus;
    }

    private void OnEnable()
    {
        if (ClickDetector.Instance != null)
        {
            ClickDetector.Instance.OnOcuppiedStateChanged += CheckForActivationStatus;
            suscribed = true;
        }
    }

    private void OnDisable()
    {
        ClickDetector.Instance.OnOcuppiedStateChanged -= CheckForActivationStatus;
    }
}