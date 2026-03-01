using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpositionSystem: MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject ExpositionPanel;
    [SerializeField] private Image ExpositionImage;
    [SerializeField] private TextMeshProUGUI ExpositionText;

    private bool isShowing = false;
    private bool suscribed = false;

    private void Start()
    {
        if (!suscribed)
        {
            ClickDetector.Instance.OnClickableExpositionObject += Show;
            ClickDetector.Instance.OnSimpleClickPerformed += () => { if (isShowing) Hide(); };
        }
    }

    public void Show(ExpositionObject expObj)
    {
        if (expObj.Image != null)
        {
            ExpositionImage.sprite = expObj.Image;
            ExpositionImage.gameObject.SetActive(true);
        }
        else
        {
            ExpositionImage.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(expObj.Text))
        {
            ExpositionText.text = expObj.Text;
            ExpositionText.gameObject.SetActive(true);
        }
        else
        {
            ExpositionText.gameObject.SetActive(false);
        }

        ExpositionPanel.SetActive(true);
        isShowing = true;
        TimeManager.Instance.ToggleTimeStop();
    }

    public void Hide()
    {
        ExpositionPanel.SetActive(false);
        isShowing = false;
        ClickDetector.Instance.UpdateOcuppiedState(false);
        TimeManager.Instance.ToggleTimeStop();
    }

    private void OnEnable()
    {
        if (ClickDetector.Instance != null)
        {
            ClickDetector.Instance.OnClickableExpositionObject += Show;
            ClickDetector.Instance.OnSimpleClickPerformed += () => { if (isShowing) Hide(); };

            suscribed = true;
        }
    }

    private void OnDisable()
    {
        ClickDetector.Instance.OnClickableExpositionObject -= Show;
        ClickDetector.Instance.OnSimpleClickPerformed -= () => { if (isShowing) Hide(); };
    }
}