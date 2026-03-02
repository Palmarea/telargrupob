using UnityEngine;

public class MotorcycleHazardSystem : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private WindowController windowController;

    [Header("Parameters")]
    [SerializeField] private float maxReactionTime = 15f;
    [SerializeField] private float minTimeBetweenHazards = 120f;
    [SerializeField] private float maxTimeBetweenHazards = 240f;

    private float hazardTimer;
    private float reactionTimer;

    private bool hazardActive = false;
    private bool waitingForNextHazard = true;

    private void Start()
    {
        ScheduleNextHazard();
    }

    private void Update()
    {
        if (TimeManager.Instance.TimeStop) return;

        UpdateHazardStatus();
    }

    private void UpdateHazardStatus()
    {
        if (waitingForNextHazard)
        {
            hazardTimer -= Time.deltaTime;

            if (hazardTimer <= 0f)
            {
                ActivateHazard();
            }
        }
        else if (hazardActive)
        {
            reactionTimer -= Time.deltaTime;

            if (!windowController.IsOpen())
            {
                ResolveHazard(true);
                return;
            }

            if (reactionTimer <= 0f)
            {
                ResolveHazard(false);
            }
        }
    }

    private void ActivateHazard()
    {
        hazardActive = true;
        waitingForNextHazard = false;
        reactionTimer = maxReactionTime;

        Debug.Log("Motorcycle Hazard ACTIVE!");
    }

    private void ResolveHazard(bool survived)
    {
        hazardActive = false;

        if (survived)
        {
            Debug.Log("Hazard avoided!");
        }
        else
        {
            Debug.Log("Hazard failed!");
        }

        ScheduleNextHazard();
    }

    private void ScheduleNextHazard()
    {
        hazardTimer = Random.Range(minTimeBetweenHazards, maxTimeBetweenHazards);
        waitingForNextHazard = true;
    }
}