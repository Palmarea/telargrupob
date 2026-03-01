using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    private float CurrentMoney;
    
    [Header("Paramenters")]
    [SerializeField] private float InitialMoney = 0;
    [SerializeField] private FMODEventSO MoneyCollectedSound;

    // Events
    public event Action<float> OnMoneyUpdated;

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
            CurrentMoney = InitialMoney;
            OnMoneyUpdated?.Invoke(CurrentMoney);
        }
        
    }
    #endregion

    public void AddMoney(float amount)
    {
        CurrentMoney += amount;
        OnMoneyUpdated?.Invoke(CurrentMoney);
        AudioEvents.OnPlayOneShot?.Invoke(MoneyCollectedSound, this.transform.position);
    }

    public void ReduceMoney(float amount)
    {
        CurrentMoney -= amount;
        OnMoneyUpdated?.Invoke(CurrentMoney);
    }

    public float GetCurrentMoney() => CurrentMoney;

    public float GetRoundedMoney() => Mathf.Round(CurrentMoney * 100f) / 100f;

}
