using System;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;

    [Header("Dependencies")]
    [SerializeField] private Animator TransitionAnimation;
    [SerializeField] private List<GameObject> ObjectsToDisableOnDepleted = new List<GameObject>();

    public event Action OnFadeToBlackFinished;
    public event Action OnFadeToTransparentFinished;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public void HandleDepleted()
    {
        foreach (GameObject go in ObjectsToDisableOnDepleted)
        {
            go.SetActive(false);
        }
    }

    public void RequestFadeToBlack()
    {
        TransitionAnimation.SetTrigger("ToBlack");
    }

    public void RequestFadeToTransparent()
    {
        TransitionAnimation.SetTrigger("ToTransparent");
    }

    public void FadeToBlackFinished()
    {
        OnFadeToBlackFinished?.Invoke();
    }

    public void FadeToTransparentFinished()
    {
        OnFadeToTransparentFinished?.Invoke();
    }
}