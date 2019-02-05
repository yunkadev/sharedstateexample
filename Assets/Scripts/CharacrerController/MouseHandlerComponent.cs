using System.Threading.Tasks;
using UnityEngine;

public class MouseHandlerComponent : SharedStateComponent
{

    public Camera cam;

    #region MonoBehaviour

    protected override void OnSharedStateChanged(SharedStateChangedEventData newState)
    {
        
    }

    protected override void OnStart()
    {
        if (cam == null)
            throw new MissingReferenceException("Объект камеры не установлен");
    }

    protected override Task[] OnUpdateAsync()
    {
        //Обрабатываем клик левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            //Берем точку по которой игрок нажал и отправляем всем компонентам уведомление
            var hit = GetMouseHit();
            return Events.PublishAsync("pointtoground", 
                new PointOnGroundEventData { Sender = this, Point = hit.point })
                .WrapToArray();
        }

        return null;
    }

    #endregion

    private RaycastHit GetMouseHit()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }
}
