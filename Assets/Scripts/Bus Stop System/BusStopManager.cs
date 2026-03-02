using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusStopManager : DayDependant
{
    [Header("Dependencies")]
    [SerializeField] private PassengerSystem PassengerSystem;
    [SerializeField] private PassengerInteractionManager PassengerInteractionManager;

    [Header("Parameters")]
    [SerializeField] private float InBetweenStopsTime = 180f;

    [Header("Resources")]
    [SerializeField] private List<TextAsset> PassengerJSONList;

    // QUITAR LUEGO DE PRUEBAS AAAAAAAAAAAAAAAAAAAAA
    [Header("Debug")]
    public bool ActivateEndless = false;
    public TextMeshProUGUI StopTimer;
    public GameObject StopPrefab;
    public Transform DebugOrigin;
    // ---------------------------------------------------------------

    private Queue<TextAsset> PassengerJSONQueue = new Queue<TextAsset>();
    private bool finished = false;
    private float timer = 0;
    private float stopTimer = 0;

    private void Awake()
    {
        foreach (TextAsset asset in PassengerJSONList)
        {
            PassengerJSONQueue.Enqueue(asset);
        }

        RegisterForDay();
    }

    public override void StartSystem()
    {
        base.StartSystem();
        PassengerSystem.SetNewPassengerQueue(PassengerJSONQueue.Dequeue());
        PassengerInteractionManager.StartPassengerInteraction();

        ArrivedToStop();

        // Detener bus
        DayManager.Instance.StartBusStop();
    }

    protected override void OnSystemUpdate()
    {
        if (TimeManager.Instance.TimeStop) return;
        
        stopTimer += Time.deltaTime;
        StopTimer.text = $"Time: {stopTimer.ToString()}";
        
        if (finished) return;

        // Si la cola de pasajeros esta vacia, desactivar UI
        if (!PassengerSystem.CheckQueueState())
        {
            PassengerInteractionManager.UpdateInterationUIState(false);

            // Si tambien esta vacias la cola de jsons, desactivar funcionalidad.
            if (PassengerJSONQueue.Count == 0)
            {
                if (ActivateEndless) // DEBUG
                {
                    RefillJSONQueue();
                }
                else
                {
                    finished = true;
                }
            }
        }
        else
        {
            if (!PassengerInteractionManager.GetInteractionState())
            {
                PassengerInteractionManager.StartPassengerInteraction();
            }
        }

        // Agregar Batch de pasajeros apartir de json cada X segundos.
        if (timer >= InBetweenStopsTime)
        {
            timer = 0;

            if (PassengerJSONQueue.Count != 0)
            {
                Debug.Log("NEW PASSENGERS ADDED");
                PassengerSystem.SetNewPassengerQueue(PassengerJSONQueue.Dequeue());
                ArrivedToStop();
                DayManager.Instance.StartBusStop();

                if (!PassengerInteractionManager.GetInteractionState())
                {
                    PassengerInteractionManager.StartPassengerInteraction();
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    #region DEBUG
    private void RefillJSONQueue()
    {
        foreach (TextAsset asset in PassengerJSONList)
        {
            PassengerJSONQueue.Enqueue(asset);
        }
    }

    private int stopIndex = 0;

    private void ArrivedToStop()
    {
        var go = Instantiate(StopPrefab, DebugOrigin.parent);

        RectTransform originRect = DebugOrigin.GetComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();

        // Copiamos configuración base
        rect.anchorMin = originRect.anchorMin;
        rect.anchorMax = originRect.anchorMax;
        rect.pivot = originRect.pivot;
        rect.localScale = Vector3.one;

        // Calculamos desplazamiento usando el ancho real del prefab
        float width = rect.rect.width;

        rect.anchoredPosition = originRect.anchoredPosition + new Vector2(width * stopIndex, 0);

        stopIndex++;

        // Color random
        Image img = go.GetComponent<Image>();
        img.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1f
        );
    }
    #endregion
}
