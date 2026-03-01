using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float LeftDeadZone = 0.3f;
    [SerializeField] private float RightDeadZone = 0.7f;
    [SerializeField] private float LeftBound = -3f;
    [SerializeField] private float RightBound = 7f;
    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private float ReturnSpeed = 15f;

    private Vector3 initialPos;

    private bool occupiedBlocked = false;
    private bool concentrationBlocked = false;

    private bool CanMove => !occupiedBlocked && !concentrationBlocked;

    private void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        if (TimeManager.Instance.TimeStop) return;
        
        float currentX = transform.position.x;
        float targetX = initialPos.x;
        float speed = ReturnSpeed;

        if (CanMove)
        {
            Vector2 mousePos = InputManager.Instance.GetRawMousePosition();
            float mouseX = mousePos.x / Screen.width;
            float direction = mouseX < LeftDeadZone ? -1 : mouseX > RightDeadZone ? 1 : 0;

            if (direction != 0)
            {
                targetX = currentX + direction * MoveSpeed * Time.deltaTime;
                targetX = Mathf.Clamp(targetX, LeftBound, RightBound);
                speed = MoveSpeed;
            }
        }

        float newX = Mathf.MoveTowards(currentX, targetX, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void CheckForOccupation(bool occupiedStatus)
    {
        occupiedBlocked = occupiedStatus;
    }

    public void ForceReturnToCenter()
    {
        concentrationBlocked = true;
    }

    public void ReleaseControl()
    {
        concentrationBlocked = false;
    }

    private void OnEnable()
    {
        if (ClickDetector.Instance != null)
            ClickDetector.Instance.OnOcuppiedStateChanged += CheckForOccupation;
    }

    private void OnDisable()
    {
        if (ClickDetector.Instance != null)
            ClickDetector.Instance.OnOcuppiedStateChanged -= CheckForOccupation;
    }
}