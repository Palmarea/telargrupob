using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyPresenter : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI TotalMoneyText;
    [SerializeField] private TextMeshProUGUI TotalMoneyTextDebug;
    
    private bool suscribed = false;

    private void Start()
    {
        if (!suscribed)
        {
            MoneyManager.Instance.OnMoneyUpdated += OnMoneyUpdated;
            OnMoneyUpdated(MoneyManager.Instance.GetCurrentMoney());
        }
    }

    private void OnMoneyUpdated(float newAmount)
    {
        ChangeText(newAmount.ToString("F2"));
    }

    private void ChangeText(string text)
    {
        text = $"Money: {text}";
        TotalMoneyText.text = text;
        TotalMoneyTextDebug.text = text;
    }

    private void OnEnable()
    {
        if (MoneyManager.Instance == null) return;

        suscribed = true;
        MoneyManager.Instance.OnMoneyUpdated += OnMoneyUpdated;
    }

    private void OnDisable()
    {
        MoneyManager.Instance.OnMoneyUpdated -= OnMoneyUpdated;
    }
}