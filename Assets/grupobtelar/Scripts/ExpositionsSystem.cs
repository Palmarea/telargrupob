using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpositionSystem: MonoBehaviour
{
    public static ExpositionSystem Instance;

    [Header("UI References")]
    public GameObject expositionPanel;
    public RawImage expositionImage;
    public TextMeshProUGUI expositionText;

    private bool isShowing = false;

    void Awake()
    {
        Instance = this;
    }

    public void Show(Texture image, string text)
    {
        if (image != null)
        {
            expositionImage.texture = image;
            expositionImage.gameObject.SetActive(true);
        }
        else
        {
            expositionImage.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(text))
        {
            expositionText.text = text;
            expositionText.gameObject.SetActive(true);
        }
        else
        {
            expositionText.gameObject.SetActive(false);
        }

        expositionPanel.SetActive(true);
        isShowing = true;
    }

    public void Hide()
    {
        expositionPanel.SetActive(false);
        isShowing = false;
    }

    public bool IsShowing()
    {
        return isShowing;
    }
}