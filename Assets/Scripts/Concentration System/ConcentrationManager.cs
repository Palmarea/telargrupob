using System;
using UnityEngine;

public class ConcentrationManager : MonoBehaviour
{
    public static ConcentrationManager Instance;
    
    [Header("Dependencies")]
    [SerializeField] private ConcentrationController Controller;
    [SerializeField] private ConcentrationSystemPresenter Presenter;

    [Header("Debug")]
    [SerializeField] private bool DebugForcedState = false;

    // Events
    public event Action OnConcentrationRefilled;
    public event Action OnConcentrationDepleted;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private void Update()
    {
        if (TimeManager.Instance.TimeStop) return;

        if (DebugForcedState) return;

        Controller.CheckForConcentrationState();
        Presenter.UpdateConcentrationPresentation(Controller.GetConcentrationValue());
    }

    private void OnEnable()
    {
        Controller.OnConcentrationRefilled += () => OnConcentrationRefilled?.Invoke();
        Controller.OnConcentrationDepleted += () => OnConcentrationDepleted?.Invoke();
    }

    private void OnDisable()
    {
        Controller.OnConcentrationRefilled -= () => OnConcentrationRefilled?.Invoke();
        Controller.OnConcentrationDepleted -= () => OnConcentrationDepleted?.Invoke();
    }
}
