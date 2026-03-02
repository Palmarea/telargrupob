using UnityEngine;

public class TransitionCanvasController : MonoBehaviour
{
    public void FadeToBlackFinished()
    {
        UIGameManager.Instance.FadeToBlackFinished();
    }

    public void FadeToTransparentFinished()
    {
        UIGameManager.Instance.FadeToTransparentFinished();
    }
}
