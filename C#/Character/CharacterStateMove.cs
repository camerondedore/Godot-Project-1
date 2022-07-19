using Godot;
using System;

public class CharacterStateMove : CharacterState
{





	public override void RunState(float delta)
	{
		// set snap to grab floor
		blackboard.snap = -blackboard.GetFloorNormal();

		
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// use camera space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.springArm.Rotation.y).Normalized();

		// project moveDirection on floor
		// var projectedMoveDirection = moveDirection;
		// var cross1 = blackboard.GetFloorNormal().Cross(moveDirection);
		// var cross2 = cross1.Cross(blackboard.GetFloorNormal());
		// projectedMoveDirection = cross2.Normalized();


		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;


		// set velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration);

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
		
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(!blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			// fall
			return blackboard.stateFall;
		}

		if(blackboard.IsOnWall() && !blackboard.IsOnFloor())
		{
			// slide
			return blackboard.stateSlide;
		}

		if(blackboard.jumpDisconnector.Trip(PlayerInput.jump) && blackboard.IsOnFloor())
		{
			// jump
			return blackboard.stateJump;
		}
		
		if(!PlayerInput.isMoving)
		{
			// move
			return blackboard.stateIdle;
		}

		return this;
	}
}
