using Godot;
using System;

public class CharacterStateIdle : CharacterState
{





    public override void RunState(float delta)
	{
		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;
		
        // set velocity
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, 0, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, 0, delta * blackboard.acceleration);

		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);

	
		// get camera look vector
		var cameraForward = -blackboard.cameraSpringArm.GlobalTransform.basis.z;
		cameraForward.y = 0;
		
		// get camera look position
		var lookPosition = cameraForward + blackboard.GlobalTransform.origin;
		
		// apply look
		blackboard.LookAt(lookPosition, Vector3.Up);
		

		// camera follow
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin, blackboard.velocity);
	}



	public override void StartState()
	{
		blackboard.snap = Vector3.Down;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
        if(blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			var canSlide = blackboard.GetSlideCollision(0).GetAngle(Vector3.Up) < blackboard.maxSlideAngle;
			if(canSlide)
			{
				// slide
				return blackboard.stateSlide;
			}

			// get start altitude
			blackboard.fallStartY = blackboard.GlobalTransform.origin.y;

			// fall
			return blackboard.stateFall;
		}

		if(!blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			// get start altitude
			blackboard.fallStartY = blackboard.GlobalTransform.origin.y;

			// fall
			return blackboard.stateFall;
		}

        if(blackboard.jumpDisconnector.Trip(PlayerInput.jump) && blackboard.IsOnFloor())
        {
            // jump start
            return blackboard.stateJumpStart;
        }
        
        if(PlayerInput.isMoving)
        {
            // move
            return blackboard.stateMove;
        }

		return this;
	}
}
