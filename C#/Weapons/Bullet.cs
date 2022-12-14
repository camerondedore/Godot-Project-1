using Godot;
using System;

public class Bullet : Spatial
{
	
	[Export]
	float damage = 10,
		speed = 100,
		speedVariation = 0,
		rangeSqr = 1000,
		gravityInfluence = 1;
	[Export]
	uint mask = 1;
	[Export]
	PackedScene hitFxScene,
		hitFxBloodScene;
	PhysicsDirectSpaceState spaceState;
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
		velocity = -GlobalTransform.basis.z.Normalized() * (speed + (GD.Randf() - 0.5f) * speedVariation);

		// get gravity
		var gravityVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		var gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
		gravity = gravityVector * gravityMagnitude;

		// get physics state
		// only works in _PhysicsProcess
		spaceState = GetWorld().DirectSpaceState;
	}



	public override void _PhysicsProcess(float delta)
	{
		// add gravity to velocity
		velocity += gravity * delta * gravityInfluence;		


		// exclude owner
		//var exclude = new Godot.Collections.Array { Owner };

		// cast ray
		var rayResult = spaceState.IntersectRay(GlobalTransform.origin, GlobalTransform.origin + velocity * delta * 1.1f, new Godot.Collections.Array { this }, mask);

		if(!rayResult.Contains("collider"))
		{
			// move and rotate bullet
			var targetPoint = GlobalTransform.origin + velocity;
			LookAtFromPosition(GlobalTransform.origin + velocity * delta, targetPoint, Vector3.Up);

			distanceTraveledSqr += velocity.LengthSquared() * delta;
		}
		else
		{
			bool hitHitbox = false;

			// get hit collider and check for hitbox
			// health needs to be the direct child of the owner and named "Health"
			var hitCollider = (Spatial) rayResult["collider"];

			if(hitCollider is Hitbox)
			{

				var hitbox = hitCollider as Hitbox;
				
				// apply damage
				hitbox.Owner.GetNode<Health>("Health").Damage(damage * hitbox.damageMultiplier);

				hitHitbox = true;
			}

			// load hit fx resource, checks for hitbox hit
			var hitFx = hitHitbox ? hitFxBloodScene.Instance() as Spatial : hitFxScene.Instance() as Spatial;

			// get hit position
			var hitPosition = (Vector3) rayResult["position"];
			
			// get hit normal
			var hitNormal = (Vector3) rayResult["normal"];

			// set parent as root scene
			//GetTree().Root.AddChild(hitFx);

			// set parent as hit collider
			hitCollider.AddChild(hitFx);

			// set hit fx position
			hitFx.LookAtFromPosition(hitPosition, hitPosition + hitNormal, Vector3.Up + velocity.Normalized());

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
