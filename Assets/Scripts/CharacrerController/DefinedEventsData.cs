

using UnityEngine;

public class PointOnGroundEventData : EventData
{
    public Vector3 Point { get; set; }
}

public class AgentMoveEventData : EventData
{
    public Vector3 DesiredVelocity { get; set; }
}