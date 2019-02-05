using System;
using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{

    private static Dispatcher _instance;
    private volatile bool _queued = false;
    private ConcurrentQueue<Action> _queue = new ConcurrentQueue<Action>();

    private static readonly object _sync_ = new object();

    //Запускает делегат в главном потоке
    public static void RunOnMainThread(Action action)
    {
        _instance._queue.Enqueue(action);
        lock (_sync_)
        {
            _instance._queued = true;
        }
    }

    //Инициализируется единственный инстанс и помечается как неудаляемый (синглтон)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_instance == null)
        {
            _instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
            DontDestroyOnLoad(_instance.gameObject);
        }
    }
  
    void Update()
    {
        if (_queued) //Выполнение очереди делегатов
        {
            while (!_queue.IsEmpty)
            {
                if (_queue.TryDequeue(out Action a))
                {
                    StartCoroutine(ActionWrapper(a));
                }
            }

            lock (_sync_)
            {
                _queued = false;
            }
        }
    }

    //Оборачивает делегат в энумератор
    IEnumerator ActionWrapper(Action a)
    {
        a();
        yield return null;
    }

}

