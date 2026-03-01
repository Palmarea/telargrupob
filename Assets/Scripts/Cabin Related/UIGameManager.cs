using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsToDisableOnDepleted = new List<GameObject>();

    public void HandleDepleted()
    {
        foreach (GameObject go in ObjectsToDisableOnDepleted)
        {
            go.SetActive(false);
        }
    }
}
