using UnityEngine;

public class ExpositionTrigger: MonoBehaviour
{
    [Header("CONTENT A MOSTRAR")]
    public Texture image;
    [TextArea]
    public string text;

    public void ShowExposition()
    {
        ExpositionSystem.Instance.Show(image, text);
    }
}
