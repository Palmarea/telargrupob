using System.Collections.Generic;
using UnityEngine;

public class PassengerSystem : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PassengerCreator Creator;

    [Header("Parameters")]
    [SerializeField] private TextAsset PassengerJSON;
    private Queue<Passenger> passengerQueue;

    private void Awake()
    {
        passengerQueue = Creator.CreatePassengerQueue(PassengerJSON);
        PrintQueue();
    }

    public void SetNewPassengerQueue(TextAsset newPassengerJSON)
    {
        PassengerJSON = newPassengerJSON;
        passengerQueue.Clear();
        passengerQueue = passengerQueue = Creator.CreatePassengerQueue(PassengerJSON);
    }

    public Passenger GetFirstPassenger() => passengerQueue.Peek();

    public void DequeueFirstPassenger() => passengerQueue.Dequeue();

    public bool CheckQueueState() => passengerQueue.Count != 0;

    private void PrintQueue()
    {
        foreach (Passenger passenger in passengerQueue)
        {
            Debug.Log(passenger.ToString());
        }
    }
}