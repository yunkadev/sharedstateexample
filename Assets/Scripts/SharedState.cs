using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SharedEvents))]
public class SharedState : MonoBehaviour
{
    //Закрытый словарь, который будет содержать все данные
    private Dictionary<string, object> _data = new Dictionary<string, object>();

    private SharedEvents _sharedEvents;

    void Start()
    {
        _sharedEvents = GetComponent<SharedEvents>();
    }


    //Индексатор для доступа к данным
    public object this[string key]
    {
        get { return _data[key]; }
        set
        {
            _data[key] = value;
            _sharedEvents.Publish("sharedstatechanged",
                new SharedStateChangedEventData { Sender = this, Field = key, NewValue = value });
        }
    }

}


public class SharedStateChangedEventData : EventData
{
    //Название параметра в общем состоянии
    public string Field { get; set; }

    //Новое значение параметра (изменение в состоянии)
    public object NewValue { get; set; }
}