using UnityEngine;
using UnityEngine.InputSystem;

public class DoorClickHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Camera passengerCamera;
    public CameraController cameraController;

    private bool isInPassengerView = false;

    void Start()
    {
        passengerCamera.enabled = false;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isInPassengerView)
            {
                passengerCamera.enabled = false;
                mainCamera.enabled = true;
                cameraController.enabled = true;
                isInPassengerView = false;
            }
            else
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject.name == "Puerta_Pasajeros")
                {
                    cameraController.enabled = false;
                    mainCamera.enabled = false;
                    passengerCamera.enabled = true;
                    isInPassengerView = true;
                }
            }
        }
    }
}