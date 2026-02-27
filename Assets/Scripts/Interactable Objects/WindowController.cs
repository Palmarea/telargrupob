using UnityEngine;

public class WindowController : MonoBehaviour
{
    private SpriteRenderer windowRenderer;
    private bool isOpen = true;

    private void Awake()
    {
        windowRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleWindow()
    {
        isOpen = !isOpen;
        Color temp = windowRenderer.color;
        temp.a = isOpen ? 0 : 1;
        windowRenderer.color = temp;
    }
}