using Godot;
using System;

public class CharacterStateIdle : CharacterState
{





    public override void RunState(float delta)
	{
		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;
		
        // set velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, 0, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, 0, delta * blackboard.acceleration);

		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


		if(blackboard.velocity.LengthSquared() > 0.2f)
		{		
			var lookDirection = new Vector2(blackboard.velocity.z, blackboard.velocity.x);
			var newRotation = blackboard.mesh.Rotation;
			
			// get angle in radians
			newRotation.y = lookDirection.Angle();
			
			// apply look
			blackboard.mesh.Rotation = newRotation;
		}
		

		// camera follow
		blackboard.springArm.Translation = blackboard.Translation;
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

			// fall
			return blackboard.stateFall;
		}

        if(blackboard.jumpDisconnector.Trip(PlayerInput.jump) && blackboard.IsOnFloor())
        {
            // jump
            return blackboard.stateJump;
        }
        
        if(PlayerInput.isMoving)
        {
            // move
            return blackboard.stateMove;
        }

		return this;
	}
}
