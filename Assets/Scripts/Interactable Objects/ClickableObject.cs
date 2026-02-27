using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    [Header("Events")]
    [Tooltip("Events called when clicked on object")]
    [SerializeField] private UnityEvent OnClicked;

    public void Click()
    {
        OnClicked?.Invoke();
    }
}