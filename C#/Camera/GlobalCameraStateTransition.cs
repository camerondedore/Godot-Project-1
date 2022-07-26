using Godot;
using System;

public class GlobalCameraStateTransition : GlobalCameraState
{

	float lerpIndex = 0;



    public override void RunState(float delta)
	{
		// move lerp index
		lerpIndex = Mathf.Clamp(lerpIndex + blackboard.smoothSpeed * delta, 0 , 1);

		// lerp to target values
        var smoothTargetPosition = blackboard.GlobalTransform.origin.LinearInterpolate(GlobalCamera.targetPosition, lerpIndex);
        var smoothTargetLookPoint = ( blackboard.GlobalTransform.origin + blackboard.GlobalTransform.basis.z * -100).LinearInterpolate(GlobalCamera.targetLookPoint, lerpIndex);

		// apply movement and look
        blackboard.LookAtFromPosition(smoothTargetPosition, smoothTargetLookPoint, Vector3.Up);
	}



	public override void StartState()
	{
		// reset lerp index
		lerpIndex = 0;

		// set starting targets
        GlobalCamera.targetPosition = blackboard.GlobalTransform.origin;
        GlobalCamera.targetLookPoint = blackboard.GlobalTransform.basis.z * -1;
        blackboard.targetSmoothSpeed = blackboard.smoothSpeed;
	}



	public override void EndState()
	{
        
	}



	public override State Transition()
	{
		if(lerpIndex == 1)
		{
			return blackboard.stateFollow;
		}

        return this;
	}
}
