using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetector: MonoBehaviour
{
    public Camera activeCamera;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = activeCamera.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                if (clickable != null)
                {
                    clickable.Click();
                }
            }
        }
    }
}