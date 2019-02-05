using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class AgentMouseController : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Camera cam;

    void Start()
    {
        //Вращение перса будет осуществляться через анимацию
        agent.updateRotation = false;
    }

    void Update()
    {
        //Получаем позицию клика на карте
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        //Если агент еще не добежал, то обновляем персу направление
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else //Если добежал, то стопаем его
        {
            character.Move(Vector3.zero, false, false);
        }
    }
}
