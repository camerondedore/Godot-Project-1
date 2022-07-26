using Godot;
using System;

public class GlobalCameraStateFollow : GlobalCameraState
{





    public override void RunState(float delta)
	{
		// apply movement and look
        blackboard.LookAtFromPosition(GlobalCamera.targetPosition, GlobalCamera.targetLookPoint, Vector3.Up);
	}



	public override void StartState()
	{

	}



	public override void EndState()
	{
        
	}



	public override State Transition()
	{
        return this;
	}
}
