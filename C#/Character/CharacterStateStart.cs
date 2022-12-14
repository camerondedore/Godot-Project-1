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
		
		// hide UI
		blackboard.characterUi.HideUi();
	}



	public override void EndState()
	{
		// set camera to start following
		GlobalCamera.camera.machine.SetState(GlobalCamera.camera.stateTransition);

		// show UI
		blackboard.characterUi.ShowUi();
	}



	public override State Transition()
	{
		var isDelayed = EngineTime.timePassed > 1.5f;
		var inputDetected = PlayerInput.isMoving || PlayerInput.isMouseMoving || PlayerInput.fire1 > 0;

        if(isDelayed && inputDetected)
        {
            // idle
            return blackboard.stateIdle;
        }

		return this;
	}
}
