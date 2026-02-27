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

    public void ToggleCameraChange()
    {
        isInPassengerView = !isInPassengerView;
        
        MainCamera.enabled = !isInPassengerView;
        MainCamera.depth = !isInPassengerView ? 1 : -1;
        MainCamera.tag = !isInPassengerView ? "MainCamera" : "Untagged";
        
        PassengerCamera.enabled = isInPassengerView;
        PassengerCamera.depth = isInPassengerView ? 1 : -1;
        PassengerCamera.tag = isInPassengerView ? "MainCamera" : "Untagged";

        CameraController.enabled = !isInPassengerView;
    }
}