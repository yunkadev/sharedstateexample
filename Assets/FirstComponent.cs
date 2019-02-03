
using UnityEngine;

public class FirstComponent : SharedStateComponent
{

    protected override void OnStart()
    {
    }


    private void OnWriteSomeDataReceived(WriteSomeDataEventData eventData)
    {
        Debug.Log(string.Format("Событие из {0}: SomeData = {1}", eventData.Sender.GetType().Name, eventData.SomeData));
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnSharedStateChanged(SharedStateChangedEventData newState)
    {
        Debug.Log(string.Format("Состояние изменено, параметр: {0}, новое значение = {1}", newState.Field, newState.NewValue));
    }
}

//Класс для передачи по подписками
public class WriteSomeDataEventData : EventData
{
    public string SomeData { get; set; }
}
