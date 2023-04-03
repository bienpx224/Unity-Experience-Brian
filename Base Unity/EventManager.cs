using System;
using System.Collections.Generic;
using UnityEngine;

/* Brian
In the first game scene, create an empty Unity GameObject named EventManager and attach the EventManager.cs script to it. 
This script is set to DontDestroyOnLoad, i.e., it won’t be destroyed when reloading scene.

=== HOW TO USE === 
- No parameter:
EventManager.TriggerEvent("gameOver", null);
- 1 parameter:
EventManager.TriggerEvent("gamePause", new Dictionary<string, object> { { "pause", true } });
- 2 or more parameters:
EventManager.TriggerEvent("addReward", 
  new Dictionary<string, object> {
    { "name", "candy" },
    { "amount", 5 } 
  });

=== How an event is published === 
void OnTriggerEnter2D(Collider2D other) {
    EventManager.TriggerEvent("addCoins", new Dictionary<string, object> { { "amount", 1 } });
  }

=== How an event is consumed example === 
public class Consumer : MonoBehaviour {
  private int coins;

  void OnEnable() {
    EventManager.StartListening("addCoins", OnAddCoins);
  }

  void OnDisable() {
    EventManager.StopListening("addCoins", OnAddCoins);
  }
  
  void OnAddCoins(Dictionary<string, object> message) {
    var amount = (int) message["amount"];
    coins += amount;
  }
}
*/
public class EventManager : MonoBehaviour
{
    private Dictionary<EventName, Action<Dictionary<string, object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();

                    //  Sets this to not be destroyed when reloading scene
                    DontDestroyOnLoad(eventManager);
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventName, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(EventName eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EventName eventName, Action<Dictionary<string, object>> listener)
    {
        if (eventManager == null) return;
        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(EventName eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
    }
}

public enum EventName
{
    EnemyKilled,
    PlayerDead,
    ClearRoom,
    ClearLevel,
}