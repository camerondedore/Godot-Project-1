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

		//moveDirection = new Plane(blackboard.GetFloorNormal(), 0).Project(moveDirection);		


		// set up velocity using persistent y
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.slopeSpeed, delta * blackboard.acceleration * 0.4f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.slopeSpeed, delta * blackboard.acceleration * 0.4f);
		
		
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
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin, blackboard.velocity);

		// add to slide time
		blackboard.slideTime += delta;
	}



	public override void StartState()
	{
		// set y to match previous velocity.y
		blackboard.ySpeed = blackboard.velocity.y - 4;

		// set snap to look for wall
		blackboard.snap = Vector3.Down;

		// reset slide time
		blackboard.slideTime = 0;
	}



	public override void EndState()
	{
	
	}



	public override State Transition()
	{
		if(blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			var cantSlide = blackboard.GetSlideCollision(0).GetAngle(Vector3.Up) > blackboard.maxSlideAngleRad;
			if(cantSlide || !blackboard.slopeRayHitCollider)
			{
				// get start altitude
				blackboard.fallStartY = blackboard.GlobalTransform.origin.y;

				// fall
				return blackboard.stateFall;
			}			
		}

		if(!blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			// get start altitude
			blackboard.fallStartY = blackboard.GlobalTransform.origin.y;

			// fall
			return blackboard.stateFall;
		}
		
		if(blackboard.IsOnFloor())
		{
			// land
			return blackboard.stateLand;
		}

		return this;
	}
}
