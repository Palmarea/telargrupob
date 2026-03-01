using System;
using FMODUnity;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    private float CurrentMoney;
    

    [Header("Paramenters")]
    [SerializeField] private float InitialMoney = 0;

    // Events
    public event Action<float> OnMoneyUpdated;

    [SerializeField] private EventReference moneyCollectedSound;

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
        SoundManager.instance.PlayOneshot(moneyCollectedSound,this.transform.position);
    }

    public void ReduceMoney(float amount)
    {
        CurrentMoney -= amount;
        OnMoneyUpdated?.Invoke(CurrentMoney);
    }

    public float GetCurrentMoney() => CurrentMoney;

    public float GetRoundedMoney() => Mathf.Round(CurrentMoney * 100f) / 100f;

}
