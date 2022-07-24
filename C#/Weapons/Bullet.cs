using Godot;
using System;

public class Bullet : Spatial
{
	
	[Export]
	float damage = 10,
		speed = 100,
		rangeSqr = 1000,
		gravityInfluence = 1;
	[Export]
	uint mask = 1;
	Vector3 velocity,
		gravity;
	float distanceTraveledSqr = 0;


	public override void _Ready()
	{
		// forward: -GlobalTransform.basis.z
		// backward: GlobalTransform.basis.z
		// left: -GlobalTransform.basis.x
		// right: GlobalTransform.basis.x

		// initialize velocity
		velocity = -GlobalTransform.basis.z.Normalized() * speed;

		// get gravity
		var gravityVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		var gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
		gravity = gravityVector * gravityMagnitude;
	}



	public override void _PhysicsProcess(float delta)
	{
		// add gravity to velocity
		velocity += gravity * delta * gravityInfluence;
		
		
		// get physics state
		// only works in _PhysicsProcess
		var spaceState = GetWorld().DirectSpaceState;

		// exclude owner
		//var exclude = new Godot.Collections.Array { Owner };

		// cast ray
		var rayResult = spaceState.IntersectRay(GlobalTransform.origin, GlobalTransform.origin + velocity * delta, new Godot.Collections.Array { this }, mask);

		if(!rayResult.Contains("collider"))
		{
			// move and rotate bullet
			var targetPoint = GlobalTransform.origin + velocity;
			LookAtFromPosition(GlobalTransform.origin + velocity * delta, targetPoint, Vector3.Up);

			distanceTraveledSqr += velocity.LengthSquared() * delta;
		}
		else
		{
			// destroy on hit
			QueueFree();
		}		


		// destroy at max range
		if(distanceTraveledSqr > rangeSqr)
		{
			QueueFree();
		}
	}
}
