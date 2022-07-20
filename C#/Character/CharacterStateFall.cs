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
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.springArm.Rotation.y).Normalized();


		// apply gravity using persistent y
		blackboard.y += blackboard.gravity * delta;
				

		// set up velocity using persistent y
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration * 0.5f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration * 0.5f);
		blackboard.velocity.y = blackboard.y;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlide(blackboard.velocity, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


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
		// set y to match previous velocity.y
		blackboard.y = blackboard.velocity.y;

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
			// move
			return blackboard.stateMove;
		}

		return this;
	}
}
