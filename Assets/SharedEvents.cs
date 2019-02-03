using System;
using System.Collections.Generic;
using UnityEngine;

public class SharedEvents : MonoBehaviour
{
    //Хранилище подписок и делегатов на их обработчики
    private readonly Dictionary<string, List<Delegate>> _subscribers = new Dictionary<string, List<Delegate>>();

    //Подписывает на события с названием eventName
    public void Subscribe<T>(string eventName, Action<T> callback) where T : EventData
    {
        if (!_subscribers.ContainsKey(eventName))
        {
            var listOfDelegates = new List<Delegate>();
            _subscribers.Add(eventName, listOfDelegates);
        }
        _subscribers[eventName].Add(callback);
    }

    //Отписывает от события с названием eventName
    public void Unsubscribe<T>(string eventName, Action<T> callback) where T : EventData
    {
        if (_subscribers.ContainsKey(eventName))
        {
            var listOfDelegates = _subscribers[eventName];
            listOfDelegates.Remove(callback);
        }
    }

    //Оправка данных data подписчикам на событие eventName
    public void Publish<T>(string eventName, T data) where T : EventData
    {
        if (_subscribers.ContainsKey(eventName))
        {
            var listOfDelegates = _subscribers[eventName];

            foreach (Action<T> callback in listOfDelegates)
            {
                callback(data);
            }
        }
    }
}

//Базовый класс для данных, передаваемых через подписки-публикации
public abstract class EventData
{
    //Инициатор события (публикатор)
    public MonoBehaviour Sender { get; set; }
}