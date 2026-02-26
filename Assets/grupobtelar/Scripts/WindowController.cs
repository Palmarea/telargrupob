using UnityEngine;

public class WindowController : MonoBehaviour
{
    public SpriteRenderer windowRenderer;
    public Color openColor = new Color(0.39f, 0.58f, 0.93f, 1f);
    public Color closedColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    private bool isOpen = true;

    public void ToggleWindow()
    {
        isOpen = !isOpen;
        windowRenderer.color = isOpen ? openColor : closedColor;
    }
}