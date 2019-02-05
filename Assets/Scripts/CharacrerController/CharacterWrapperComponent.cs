using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class CharacterWrapperComponent : SharedStateComponent
{
    private ThirdPersonCharacter character;

    #region Monobehaviour

    protected override void OnSharedStateChanged(SharedStateChangedEventData newState)
    {
    }

    protected override void OnStart()
    {
        character = GetComponent<ThirdPersonCharacter>();

        Events.Subscribe<AgentMoveEventData>("agentmoved", OnAgentMove);
    }

    protected override Task[] OnUpdateAsync()
    {
        return null;
    }

    #endregion

    private void OnAgentMove(AgentMoveEventData eventData)
    {
        PerformInMainThread(() => character.Move(eventData.DesiredVelocity, false, false));
    }
}
