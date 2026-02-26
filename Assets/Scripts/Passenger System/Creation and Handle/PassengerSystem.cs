using System.Collections.Generic;
using UnityEngine;

public class PassengerSystem : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PassengerCreator Creator;
    
    private TextAsset currenPassengerJSON;
    private Queue<Passenger> passengerQueue = new Queue<Passenger>();

    public void SetNewPassengerQueue(TextAsset newPassengerJSON)
    {
        currenPassengerJSON = newPassengerJSON;

        Queue<Passenger> newQueue = Creator.CreatePassengerQueue(currenPassengerJSON);

        foreach (Passenger passenger in newQueue)
        {
            passengerQueue.Enqueue(passenger);
        }
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