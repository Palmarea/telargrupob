using System;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public static ClickDetector Instance;

    [Header("Configuration")]
    [SerializeField] private LayerMask ClickableLayerMask;

    public event Action<ExpositionObject> OnClickableExpositionObject;
    public event Action OnSimpleClickPerformed;
    public event Action<bool> OnOcuppiedStateChanged;

    private bool ocuppied = false;

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

    private void CheckForHitClickableObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(InputManager.Instance.GetMousePosition(), Vector2.zero, ClickableLayerMask);

        if (hit.collider != null && !ocuppied)
        {
            ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.Click();

                ExpositionObject expObj = hit.collider.GetComponent<ExpositionObject>();

                if (expObj != null)
                {
                    OnClickableExpositionObject?.Invoke(expObj);
                    UpdateOcuppiedState(true);
                }
            }
        }
        else
        {
            OnSimpleClickPerformed?.Invoke();
        }
    }

    public void UpdateOcuppiedState(bool state)
    {
        ocuppied = state;
        OnOcuppiedStateChanged?.Invoke(ocuppied);
    }

    private void OnEnable()
    {
        InputManager.Instance.OnSelectPerformed += CheckForHitClickableObject;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnSelectPerformed -= CheckForHitClickableObject;
    }
}