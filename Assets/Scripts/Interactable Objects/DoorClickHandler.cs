using System.Collections.Generic;
using UnityEngine;

public class DoorClickHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Camera MainCamera;
    [SerializeField] private List<GameObject> MainCameraDependentObjects = new List<GameObject>();
    [SerializeField] private Camera PassengerCamera;
    [SerializeField] private List<GameObject> PassengerDependentObjects = new List<GameObject>();
    [SerializeField] private CameraController CameraController;

    private bool isInPassengerView = false;
    public bool IsInPassengerView => isInPassengerView;

    void Start()
    {
        ToggleCamera(PassengerCamera, PassengerDependentObjects, false);
    }

    public void ToggleCameraChange()
    {
        isInPassengerView = !isInPassengerView;

        ToggleCamera(MainCamera, MainCameraDependentObjects, !isInPassengerView);
        ToggleCamera(PassengerCamera, PassengerDependentObjects, isInPassengerView);

        CameraController.enabled = !isInPassengerView;
    }

    private void ToggleCamera(Camera target, List<GameObject> dependentObjects, bool state)
    {
        target.enabled = state;
        target.depth = state ? 1 : -1;
        target.tag = state ? "MainCamera" : "Untagged";
        ToggleDependentObjects(dependentObjects, state);
    }

    private void ToggleDependentObjects(List<GameObject> list, bool state)
    {
        foreach(GameObject dependentObj in list)
        {
            dependentObj.SetActive(state);
        }
    }
}