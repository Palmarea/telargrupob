using System;
using UnityEngine;

public class ConcentrationManager : DayDependant
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
            RegisterForDay();
            RegisterForBusStopPause();
        }
    }
    #endregion

    protected override void OnSystemUpdate()
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
