using UnityEngine;

public class WindowController : MonoBehaviour
{
    private SpriteRenderer windowRenderer;
    private bool isOpen = true;
    private Color initialColor;
    private Color hoverColor = new Color(1, 1, 1, 0.25f);

    private void Awake()
    {
        windowRenderer = GetComponent<SpriteRenderer>();
        windowRenderer.color = initialColor;
    }

    public void ToggleWindow()
    {
        isOpen = !isOpen;
        Color temp = windowRenderer.color;
        temp.a = isOpen ? 0 : 1;
        windowRenderer.color = temp;
    }

    public void OnHoverAction()
    {
        if (!isOpen) return;
        windowRenderer.color = hoverColor;
    }

    public void OffHoverAction()
    {
        if (!isOpen) return;
        windowRenderer.color = initialColor;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}