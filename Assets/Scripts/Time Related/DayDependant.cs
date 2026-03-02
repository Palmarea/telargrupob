using UnityEngine;

public abstract class DayDependant : MonoBehaviour
{
    protected bool SystemInitialized = false;
    
    private void Update()
    {
        if (!SystemInitialized) return;
        OnSystemUpdate();
    }

    public void RegisterForDay()
    {
        DayManager.Instance.RegisterForDay(this);
    }

    public void RegisterForPreDay()
    {
        DayManager.Instance.RegisterForPreDay(this);
    }

    public void RegisterForBusStopPause()
    {
        DayManager.Instance.RegisterForBusStopPause(this);
    }

    public virtual void StartSystem()
    {
        SystemInitialized = true;
    }

    public virtual void StopSystem()
    {
        SystemInitialized = false;
    }

    protected abstract void OnSystemUpdate();
}