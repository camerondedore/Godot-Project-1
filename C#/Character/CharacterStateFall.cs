using Godot;
using System;

public class CharacterStateFall : CharacterState
{





	public override void RunState(float delta)
	{
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// use camera space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.cameraSpringArm.Rotation.y).Normalized();


		// set up velocity using persistent y
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration * 0.5f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration * 0.5f);
		

		// apply gravity using persistent y
		blackboard.ySpeed += blackboard.gravity * delta;
		blackboard.velocity.y = blackboard.ySpeed;


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
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin, blackboard.velocity);
	}



	public override void StartState()
	{
		// set y to match previous velocity.y
		blackboard.ySpeed = blackboard.velocity.y;

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
		}
		
		if(blackboard.IsOnFloor())
		{
			// fell far enough to land
			if(blackboard.jumpStartY > blackboard.GlobalTransform.origin.y + blackboard.landHeight ||
                blackboard.fallStartY > blackboard.GlobalTransform.origin.y + blackboard.landHeight)
			{
				// land hard
				return blackboard.stateLandHard;
			}
			
			// land
			return blackboard.stateLand;
		}

		return this;
	}
}
