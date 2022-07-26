using Godot;
using System;

public class CharacterStateJumpPad : CharacterState
{





    public override void RunState(float delta)
	{
        // detect player input
        if(PlayerInput.isMoving)
        {
            // get input
            var moveDirection = Vector3.Zero;
            moveDirection.x = PlayerInput.move.x;
            moveDirection.z = PlayerInput.move.z;

            // use camera space
            moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.cameraSpringArm.Rotation.y).Normalized();

    		// set velocity adding input on top of the jump pad velicoty
            blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, 
                blackboard.jumpPadVelocity.x + moveDirection.x * blackboard.speed, delta * blackboard.jumpPadAcceleration);
            blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, 
                blackboard.jumpPadVelocity.z + moveDirection.z * blackboard.speed, delta * blackboard.jumpPadAcceleration);
        }


        // apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlide(blackboard.velocity, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


		// get camera look vector
		var cameraForward = -blackboard.cameraSpringArm.GlobalTransform.basis.z;
		cameraForward.y = 0;
		
		// get camera look position
		var lookPosition = cameraForward + blackboard.GlobalTransform.origin;
		
		// apply look
		blackboard.LookAt(lookPosition, Vector3.Up);


		// camera follow
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin);
	}



	public override void StartState()
	{
		// set velocity
		blackboard.velocity = blackboard.jumpPadVelocity;
		
		// set snap to zero to release from floor
		blackboard.snap = Vector3.Zero;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			var canSlide = blackboard.GetSlideCollision(0).GetAngle(Vector3.Up) < blackboard.maxSlideAngleRad;
			if(canSlide)
			{
				// slide
				return blackboard.stateSlide;
			}

			// fall
            return blackboard.stateFall;
		}

        if(blackboard.IsOnFloor())
        {
            // move
            return blackboard.stateMove;
        }

		return this;
	}
}