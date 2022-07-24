using Godot;
using System;

public class CharacterStateSlide : CharacterState
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
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.slopeSpeed, delta * blackboard.acceleration * 0.5f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.slopeSpeed, delta * blackboard.acceleration * 0.5f);
		
		
		// apply gravity using persistent y
		blackboard.ySpeed += blackboard.gravity * delta;
		blackboard.velocity.y = blackboard.ySpeed;


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
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin);


		// resume camera y
		if(blackboard.cameraSpringArm.freezeY && (blackboard.GlobalTransform.origin.y < blackboard.jumpStartY 
			|| blackboard.GlobalTransform.origin.y > blackboard.jumpStartY + blackboard.jumpHeight))
		{
			blackboard.cameraSpringArm.freezeY = false;
		}
	}



	public override void StartState()
	{
		// set y to match previous velocity.y
		blackboard.ySpeed = blackboard.velocity.y;

		// set snap to look for wall
		blackboard.snap = Vector3.Down;

		// freeze camera y
		blackboard.cameraSpringArm.freezeY = true;
	}



	public override void EndState()
	{
		// resume camera y
		blackboard.cameraSpringArm.freezeY = false;
	}



	public override State Transition()
	{
		if(blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			var cantSlide = blackboard.GetSlideCollision(0).GetAngle(Vector3.Up) > blackboard.maxSlideAngleRad;
			if(cantSlide)
			{
				// fall
				return blackboard.stateFall;
			}			
		}
		
		if(blackboard.IsOnFloor())
		{
			// move
			return blackboard.stateMove;
		}

		return this;
	}
}
