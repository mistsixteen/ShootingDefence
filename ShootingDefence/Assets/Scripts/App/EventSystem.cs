using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : MonoBehaviour
{
    private static EventSystem instance;

    //TODO : ���� UnityAction<objcet[]>���� �ٲ� �� arg �����ϴ°͵� ���
    private Dictionary<EventType, UnityAction> eventDict;

    public static EventSystem GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        eventDict = new Dictionary<EventType, UnityAction>();
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void RegistEventListener(EventType eventName, UnityAction eventAction)
    {
        if(!eventDict.ContainsKey(eventName))
        {
            UnityAction newUnityAction = null;
            eventDict.Add(eventName, newUnityAction);
        }
        eventDict[eventName] += eventAction;
    }

    public void UnRegistEventListener(EventType eventName, UnityAction eventAction)
    {
        if (!eventDict.ContainsKey(eventName))
        {
            UnityAction newUnityAction = null;
            eventDict.Add(eventName, newUnityAction);
        }
        eventDict[eventName] -= eventAction;
    }

    public void InvokeEvent(EventType eventName)
    {
        if (eventDict.ContainsKey(eventName))
        {
            var unityAction = eventDict[eventName];
            unityAction?.Invoke();
        }
    }

}
