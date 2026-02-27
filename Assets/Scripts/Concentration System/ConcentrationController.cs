using UnityEngine;

public class ConcentrationController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform CenterReferencePoint;

    [Header("Parameters")]
    [SerializeField] private float MaxDistance = 3f;
    [SerializeField] private float RefillTime = 5.0f;
    [SerializeField] private float DepletedTime = 15.0f;

    private void Update()
    {
        CheckForConcentrationState();
    }

    public void CheckForConcentrationState()
    {
        if (Vector2.Distance(CenterReferencePoint.position, InputManager.Instance.GetMousePosition()) <= MaxDistance)
        {
            Debug.Log("Concentrado");
        }
        else
        {
            Debug.Log("Desconcentrado");
        }
    }
}
