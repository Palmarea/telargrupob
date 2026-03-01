using UnityEngine;

public class CameraGameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private DoorClickHandler DoorHandler;
    [SerializeField] private CameraController MainCameraController;
    [SerializeField] private UIGameManager UIManager;

    private bool suscribed = false;

    private void Start()
    {
        if (!suscribed)
        {
            ConcentrationManager.Instance.OnConcentrationDepleted += HandleDepleted;
            ConcentrationManager.Instance.OnConcentrationRefilled += HandleRefilled;
            suscribed = true;
        }
    }

    private void HandleDepleted()
    {
        if (DoorHandler.IsInPassengerView)
        {
            DoorHandler.ToggleCameraChange();
        }

        UIManager.HandleDepleted();
        MainCameraController.ForceReturnToCenter();
    }

    private void HandleRefilled()
    {
        MainCameraController.ReleaseControl();
    }

    private void OnEnable()
    {
        if (ConcentrationManager.Instance != null)
        {
            ConcentrationManager.Instance.OnConcentrationDepleted += HandleDepleted;
            ConcentrationManager.Instance.OnConcentrationRefilled += HandleRefilled;
            suscribed = true;
        }
    }

    private void OnDisable()
    {
        if (ConcentrationManager.Instance != null)
        {
            ConcentrationManager.Instance.OnConcentrationDepleted -= HandleDepleted;
            ConcentrationManager.Instance.OnConcentrationRefilled -= HandleRefilled;
        }
    }
}