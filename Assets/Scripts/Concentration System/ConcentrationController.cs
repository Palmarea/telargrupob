using System;
using UnityEngine;

public class ConcentrationController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform CenterReferencePoint;

    [Header("Parameters")]
    [SerializeField] private int InitialConcentration = 100;
    [SerializeField] private float MaxDistance = 3f;
    [SerializeField] private float RefillTime = 5.0f;
    [SerializeField] private float DepletedTime = 15.0f;

    private bool refilling = false;
    private float concentrationValue;
    private float concentrationMaxValue;
    private float concentrationMinValue = 0;

    private float concentrationRefillRate;
    private float concentrationDepletedRate;

    private float marginErrorValue = 1f;

    // Events
    public event Action OnConcentrationRefilled;
    public event Action OnConcentrationDepleted;

    private void Start()
    {
        concentrationValue = InitialConcentration;
        concentrationMaxValue = concentrationValue;

        concentrationRefillRate = concentrationValue / RefillTime;
        concentrationDepletedRate = concentrationValue / DepletedTime;
    }

    public void CheckForConcentrationState()
    {
        bool isConcentrated =
            Vector2.Distance(
                CenterReferencePoint.position,
                InputManager.Instance.GetMousePosition()
            ) <= MaxDistance;

        if (isConcentrated)
        {
            concentrationValue += concentrationRefillRate * Time.deltaTime;
        }
        else
        {
            concentrationValue -= concentrationDepletedRate * Time.deltaTime;
        }

        concentrationValue = Mathf.Clamp(
            concentrationValue,
            concentrationMinValue,
            concentrationMaxValue
        );

        if (concentrationValue <= concentrationMinValue + marginErrorValue && !refilling)
        {
            concentrationValue = concentrationMinValue;
            refilling = true;
            OnConcentrationDepleted?.Invoke();
        }

        if (concentrationValue >= concentrationMaxValue - marginErrorValue && refilling)
        {
            concentrationValue = concentrationMaxValue;
            refilling = false;
            OnConcentrationRefilled?.Invoke();
        }
    }

    public float GetConcentrationValue() => concentrationValue;
}
