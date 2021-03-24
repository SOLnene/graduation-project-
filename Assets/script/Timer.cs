using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float statsMultiplier = 1f;
    private class TimedEvent
    {
        public float TimeToExecute;
        public Callback Method;
    }

    private List<TimedEvent> events;
    
    public delegate void Callback();

    void Awake()
    {
        events = new List<TimedEvent>();
    }

    private void Start()
    {
        InvokeRepeating("onEnemyIntensify", 0f, 2f);
    }
    public void Add(Callback method,float inSeconds)
    {
        events.Add(new TimedEvent {
            Method = method,
            TimeToExecute = Time.time + inSeconds
        });
    }

    public void onEnemyIntensify()
    {
        statsMultiplier += 0.2f;
    }

    void Update()
    {
        if(events.Count == 0)
        {
            return;
        }
        
        for(int i = 0; i < events.Count; i++)
        {
            var timeEvent = events[i];
            if (timeEvent.TimeToExecute <= Time.time)
            {
                timeEvent.Method();
                events.Remove(timeEvent);
                
            }
        }
    }
}
