using Godot;
using System;

public class MovingPlatformSimple : KinematicBody
{
	[Export]
	float speed = 2;
	[Export]
	Vector3 targetOffset;
	Vector3 startPosition,
		endPosition,
		targetPosition;



	public override void _Ready()
	{
		// initialize vectors
		startPosition = Translation;
		endPosition = startPosition + targetOffset;
		targetPosition = endPosition;
	}



	public override void _PhysicsProcess(float delta)
	{
		// get vector to target
		var vectorToTarget = targetPosition - Translation;

		// get velocity per tick
		var velocity = vectorToTarget.Normalized() * speed * delta;

		// check of velocity per tick will overshoot target
		// this is similar to Unity's MoveTowards method
		if(vectorToTarget.LengthSquared() < velocity.LengthSquared())
		{
			// set velocity per tick to "perfectly" reach target
			velocity = vectorToTarget;
		}
		
		// apply movement
		Translate(velocity);

		// check for arriving at target
		if(Translation == targetPosition)
		{
			if(targetPosition == endPosition)
			{
				// at end, target is now start
				targetPosition = startPosition;
			}
			else
			{
				// at start, target is now end
				targetPosition = endPosition;
			}
		}
	}
}
