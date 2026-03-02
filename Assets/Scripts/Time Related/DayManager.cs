using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("Day Durations")]
    [SerializeField] private float TotalDayDuration = 480f;
    [SerializeField] private float DepotDuration = 30f;
    [SerializeField] private float AdvantageTime = 20f;

    [Header("Bus Stop")]
    [SerializeField] private float DefaultBusStopDuration = 8f;

    private enum DayPhase
    {
        None,
        Morning,
        Afternoon,
        Night,
        GoingToDepot,
        Ending
    }

    private DayPhase currentPhase = DayPhase.None;

    private List<DayDependant> PreDayDependantsSystems = new();
    private List<DayDependant> DayDependantsSystems = new();
    private List<DayDependant> StopSensitiveSystems = new();

    private float advantageTimer;
    private float dayTimer;
    private float depotTimer;

    private bool predayStarted = false;
    private bool dayStarted = false;
    private bool isTransitioning = false;

    // 🚌 BUS STOP
    private bool busStopped = false;
    private float busStopTimer = 0f;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        advantageTimer = AdvantageTime;
        dayTimer = 0f;
    }
    #endregion

    private void Start()
    {
        if (UIGameManager.Instance != null)
        {
            UIGameManager.Instance.OnFadeToBlackFinished += HandleFadeBlackFinished;
            UIGameManager.Instance.OnFadeToTransparentFinished += HandleFadeTransparentFinished;
        }
    }

    private void Update()
    {
        //  Pausa global narrativa
        if (TimeManager.Instance.TimeStop) return;

        HandlePreDay();

        if (!dayStarted) return;

        HandleBusStop();       // 👈 primero la parada
        HandleDayProgress();   // 👈 luego progreso normal
    }

    #region PRE DAY

    private void HandlePreDay()
    {
        if (!predayStarted || dayStarted) return;

        if (advantageTimer <= 0)
        {
            StartDay();
        }
        else
        {
            advantageTimer -= Time.deltaTime;
        }
    }

    #endregion

    #region DAY FLOW

    private void HandleDayProgress()
    {
        if (busStopped) return; // 👈 el día no avanza mientras está detenido

        dayTimer += Time.deltaTime;
        float normalized = dayTimer / TotalDayDuration;

        if (currentPhase == DayPhase.Morning && normalized >= 0.33f)
            StartPhaseTransition(DayPhase.Afternoon);

        else if (currentPhase == DayPhase.Afternoon && normalized >= 0.66f)
            StartPhaseTransition(DayPhase.Night);

        else if (currentPhase == DayPhase.Night && normalized >= 1f)
            StartGoingToDepot();

        if (currentPhase == DayPhase.GoingToDepot)
        {
            depotTimer -= Time.deltaTime;

            if (depotTimer <= 0)
                StartEndDay();
        }
    }

    private void StartDay()
    {
        dayStarted = true;
        currentPhase = DayPhase.Morning;

        foreach (var dep in DayDependantsSystems)
            dep.StartSystem();
    }

    private void StartPhaseTransition(DayPhase nextPhase)
    {
        if (isTransitioning) return;

        isTransitioning = true;
        currentPhase = nextPhase;

        TimeManager.Instance.ToggleTimeStop();
        UIGameManager.Instance.RequestFadeToBlack();
    }

    private void StartGoingToDepot()
    {
        if (isTransitioning) return;

        currentPhase = DayPhase.GoingToDepot;
        depotTimer = DepotDuration;

        Debug.Log("Going to depot...");
    }

    private void StartEndDay()
    {
        currentPhase = DayPhase.Ending;
        isTransitioning = true;

        foreach (var dep in PreDayDependantsSystems)
            dep.StopSystem();

        foreach (var dep in DayDependantsSystems)
            dep.StopSystem();

        TimeManager.Instance.ToggleTimeStop();
        UIGameManager.Instance.RequestFadeToBlack();
    }

    #endregion

    #region 🚌 BUS STOP SYSTEM

    public void StartBusStop(float duration = -1f)
    {
        if (busStopped) return;

        busStopped = true;
        busStopTimer = duration > 0 ? duration : DefaultBusStopDuration;

        foreach (var system in StopSensitiveSystems)
            system.StopSystem();

        Debug.Log(" Bus stopped at station");
    }

    private void HandleBusStop()
    {
        if (!busStopped) return;

        busStopTimer -= Time.deltaTime;

        if (busStopTimer <= 0)
            EndBusStop();
    }

    private void EndBusStop()
    {
        busStopped = false;

        foreach (var system in StopSensitiveSystems)
            system.StartSystem();

        Debug.Log(" Bus leaving station");
    }

    #endregion

    #region Animation Events (FADES RESTAURADOS)

    private void HandleFadeBlackFinished()
    {
        if (currentPhase == DayPhase.Ending)
        {
            GameManager.Instance.GameEnd();
            return;
        }

        UIGameManager.Instance.RequestFadeToTransparent();
    }

    private void HandleFadeTransparentFinished()
    {
        if (currentPhase != DayPhase.None)
        {
            TimeManager.Instance.ToggleTimeStop();
        }

        isTransitioning = false;
    }

    #endregion

    #region Public API

    public void PreStartDay()
    {
        predayStarted = true;

        foreach (var dep in PreDayDependantsSystems)
            dep.StartSystem();
    }

    public void RegisterForPreDay(DayDependant system)
        => PreDayDependantsSystems.Add(system);

    public void RegisterForDay(DayDependant system)
        => DayDependantsSystems.Add(system);

    public void RegisterForBusStopPause(DayDependant system)
        => StopSensitiveSystems.Add(system);

    #endregion
}