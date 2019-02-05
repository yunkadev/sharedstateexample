using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentWrapperComponent : SharedStateComponent
{ 
    private NavMeshAgent agent;

    #region Monobehaviour

    protected override void OnSharedStateChanged(SharedStateChangedEventData newState)
    {
        
    }

    protected override void OnStart()
    {
        //Получаем агента
        agent = GetComponent<NavMeshAgent>();

        //Вращение перса будет осуществляться через анимацию
        agent.updateRotation = false;

        Events.Subscribe<PointOnGroundEventData>("pointtoground", OnPointToGroundGot);
    }

    protected override Task[] OnUpdateAsync()
    {
        //Передача состояния по позиции агента
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            return Events.PublishAsync("agentmoved",
                new AgentMoveEventData { Sender = this, DesiredVelocity = agent.desiredVelocity })
                .WrapToArray();
        }
        else
        {
            return Events.PublishAsync("agentmoved",
                new AgentMoveEventData { Sender = this, DesiredVelocity = Vector3.zero })
                .WrapToArray();
        }
    }

    #endregion

    private void OnPointToGroundGot(PointOnGroundEventData eventData)
    {
        //Назначаем агенту новую позицию
        PerformInMainThread(() => agent.SetDestination(eventData.Point));
    }
}
