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
            MouseController.Instance.OnSimpleClickPerformed += () => { if (isOpen) ToggleCellphone(); };
        }
    }

    public void ToggleCellphone()
    {
        isOpen = !isOpen;
        celularPanel.SetActive(isOpen);
        MouseController.Instance.UpdateOcuppiedState(isOpen);
    }

    private void OnEnable()
    {
        if (MouseController.Instance != null)
        {
            MouseController.Instance.OnSimpleClickPerformed += () => { if (isOpen) ToggleCellphone(); };
            suscribed = true;
        }
    }

    private void OnDisable()
    {
        MouseController.Instance.OnSimpleClickPerformed -= () => { if (isOpen) ToggleCellphone(); };
    }
}
