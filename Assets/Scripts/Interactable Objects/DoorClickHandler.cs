using UnityEngine;

public class DoorClickHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera PassengerCamera;
    [SerializeField] private CameraController CameraController;

    private bool isInPassengerView = false;

    void Start()
    {
        PassengerCamera.depth = -1;
        PassengerCamera.enabled = false;
    }

    //void Update()
    //{
    //    if (Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        if (isInPassengerView)
    //        {
    //            PassengerCamera.enabled = false;
    //            MainCamera.enabled = true;
    //            CameraController.enabled = true;
    //            isInPassengerView = false;
    //        }
    //        else
    //        {
    //            Vector2 mousePos = Mouse.current.position.ReadValue();
    //            Vector2 worldPos = MainCamera.ScreenToWorldPoint(mousePos);
    //            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

    //            if (hit.collider != null && hit.collider.gameObject.name == "Puerta_Pasajeros")
    //            {
    //                CameraController.enabled = false;
    //                MainCamera.enabled = false;
    //                PassengerCamera.enabled = true;
    //                isInPassengerView = true;
    //            }
    //        }
    //    }
    //}

    public void ToggleCameraChange()
    {
        isInPassengerView = !isInPassengerView;
        
        MainCamera.enabled = !isInPassengerView;
        MainCamera.depth = !isInPassengerView ? 1 : -1;
        
        PassengerCamera.enabled = isInPassengerView;
        PassengerCamera.depth = isInPassengerView ? 1 : -1;

        CameraController.enabled = !isInPassengerView;
    }
}