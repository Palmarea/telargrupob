using System.Collections.Generic;
using UnityEngine;

public class BusStopManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PassengerSystem PassengerSystem;
    [SerializeField] private PassengerInteractionManager PassengerInteractionManager;

    [Header("Parameteres")]
    [SerializeField] private float InBetweenStopsTime = 180f;

    [Header("Resources")]
    [SerializeField] private List<TextAsset> PassengerJSONList;

    private Queue<TextAsset> PassengerJSONQueue = new Queue<TextAsset>();
    private bool finished = false;
    private float timer = 0;

    private void Awake()
    {
        foreach (TextAsset asset in PassengerJSONList)
        {
            PassengerJSONQueue.Enqueue(asset);
        }

        PassengerSystem.SetNewPassengerQueue(PassengerJSONQueue.Dequeue());
        PassengerInteractionManager.StartPassengerInteraction();
    }

    private void Update()
    {
        if (finished) return;

        // Si la cola de pasajeros esta vacia, desactivar UI
        if (!PassengerSystem.CheckQueueState())
        {
            PassengerInteractionManager.UpdateInterationUIState(false);

            // Si tambien esta vacias la cola de jsons, desactivar funcionalidad.
            if (PassengerJSONQueue.Count == 0) finished = true;
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
}
