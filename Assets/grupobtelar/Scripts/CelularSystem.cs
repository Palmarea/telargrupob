using UnityEngine;

public class CelularSystem: MonoBehaviour
{
    public GameObject celularPanel;
    private bool isOpen = false;

    public void TggleCelular()
    {
        isOpen = !isOpen;
        celularPanel.SetActive(isOpen);
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
