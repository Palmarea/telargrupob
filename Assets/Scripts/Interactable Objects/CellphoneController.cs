using UnityEngine;

public class CellphoneController: MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject celularPanel;
    private bool isOpen = false;
    private bool suscribed = false;

    private void Start()
    {
        if (!suscribed)
        {
            ClickDetector.Instance.OnSimpleClickPerformed += () => { if (isOpen) ToggleCellphone(); };
        }
    }

    public void ToggleCellphone()
    {
        isOpen = !isOpen;
        celularPanel.SetActive(isOpen);
        ClickDetector.Instance.UpdateOcuppiedState(isOpen);
    }

    private void OnEnable()
    {
        if (ClickDetector.Instance != null)
        {
            ClickDetector.Instance.OnSimpleClickPerformed += () => { if (isOpen) ToggleCellphone(); };
            suscribed = true;
        }
    }

    private void OnDisable()
    {
        ClickDetector.Instance.OnSimpleClickPerformed -= () => { if (isOpen) ToggleCellphone(); };
    }
}
