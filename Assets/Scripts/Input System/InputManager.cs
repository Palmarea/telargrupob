using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInput InputHandler;

    // Events
    public event Action OnSelectPerformed;

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
            if (InputHandler == null) InputHandler = GetComponent<PlayerInput>();
        }
    }
    #endregion

    private void Update()
    {
        Debug.Log(GetMousePosition());
    }

    public Vector2 GetMousePosition() => InputHandler.GetMousePosition();

    private void Click()
    {
        Debug.Log("Has been clicked");
    }

    private void OnEnable()
    {
        InputHandler.OnSelectPerformed += Click;
    }

    private void OnDisable()
    {
        InputHandler.OnSelectPerformed -= Click;
    }
}

