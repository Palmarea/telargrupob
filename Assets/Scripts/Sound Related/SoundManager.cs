using UnityEngine;
using FMODUnity;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}

    public void Awake()
    {

        instance=this;       
    }

    public void PlayOneshot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }
}