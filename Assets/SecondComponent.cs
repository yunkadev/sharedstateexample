using UnityEngine;

public class SecondComponent : SharedStateComponent
{

    protected override void OnStart()
    {
        //Выполняем метод через 1 секнд
        Invoke("ChangeSharedState", 1);
    }

    void ChangeSharedState()
    {
        State["somedata"] = 500;
    }


    protected override void OnUpdate()
    {

    }

    //Метод который выполняет публикацию события
    void FireWriteSomeDataEvent()
    {
        Events.Publish("writesomedata", new WriteSomeDataEventData { Sender = this, SomeData = "Привет из SecondComponent" });
    }

    protected override void OnSharedStateChanged(SharedStateChangedEventData newState)
    {
        Debug.Log(string.Format("Состояние изменено, параметр: {0}, новое значение = {1}", newState.Field, newState.NewValue));
    }
}
