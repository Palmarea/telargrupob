using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    [Header("Evento al hacer clic")]
    public UnityEvent OnClicked;

    public void Click()
    {
        OnClicked?.Invoke();
    }
}