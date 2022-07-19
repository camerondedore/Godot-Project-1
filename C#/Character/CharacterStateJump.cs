using Godot;
using System;

public class CharacterStateJump : CharacterState
{





    public override void RunState(float delta)
	{
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// use camera space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.springArm.Rotation.y).Normalized();


		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;


		// set velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration * 0.5f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration * 0.5f);

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
		// set vertical speed; v = (-2hg)>(1/2)
		blackboard.velocity.y = Mathf.Sqrt((-2 * blackboard.jumpHeight * blackboard.gravity));
		// set snap to zero to release from floor
		blackboard.snap = Vector3.Zero;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		 if(blackboard.velocity.y < 0 || blackboard.IsOnWall() || blackboard.IsOnFloor())
        {
            // fall
            return blackboard.stateFall;
        }

		return this;
	}
}