using Godot;
using System;

public class CharacterStateStart : CharacterState
{





    public override void RunState(float delta)
	{
		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;

		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);
	}



	public override void StartState()
	{
		blackboard.snap = Vector3.Down;
	}



	public override void EndState()
	{
		// set camera to start following
		GlobalCamera.camera.machine.SetState(GlobalCamera.camera.stateTransition);
	}



	public override State Transition()
	{
		var isDelayed = OS.GetTicksMsec() * 0.001f > 1.5f;
		var inputDetected = PlayerInput.isMoving || PlayerInput.look.LengthSquared() > 0 || PlayerInput.fire1 > 0;

        if(isDelayed && inputDetected)
        {
            // idle
            return blackboard.stateIdle;
        }

		return this;
	}
}
