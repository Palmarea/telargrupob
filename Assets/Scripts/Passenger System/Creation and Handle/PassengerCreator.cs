using System.Collections.Generic;
using UnityEngine;

public struct Passengers
{
    public Passenger[] passengers;
}

public class PassengerCreator : MonoBehaviour
{
    public Queue<Passenger> CreatePassengerQueue(TextAsset PassengerJSON)
    {
        Queue<Passenger> queue = new Queue<Passenger>();

        Passengers deserializedPassengers = JsonUtility.FromJson<Passengers>(PassengerJSON.text);

        foreach(Passenger person in deserializedPassengers.passengers)
        {
            queue.Enqueue(person);
        }

        return queue;
    }
}