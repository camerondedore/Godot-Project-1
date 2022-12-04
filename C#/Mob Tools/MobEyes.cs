using Godot;
using System;

public class MobEyes : RayCast
{
	
	//[Export]
	//NodePath debugPath;
	[Export]
	float viewRange = 25;
	//uint mask = 1;

	float viewRangeSquared;

   
   
	public override void _Ready()
	{
		// get nodes
		//debug = GetNode<Spatial>(debugPath);

		viewRangeSquared = viewRange * viewRange;
	}



	public bool CanSeeTarget(Spatial target)
	{
		// set forward to world
		// CRITICAL TO RAY CAST
		LookAt(GlobalTransform.origin + Vector3.Forward, Vector3.Up);

		// check distance
		if(target.GlobalTransform.origin.DistanceSquaredTo(GlobalTransform.origin) > viewRangeSquared)
		{
			// too far
			return false;
		}

		// get target position
		CastTo = target.GlobalTransform.origin - GlobalTransform.origin;

		// cast ray
		ForceRaycastUpdate();

		// check collision
		if(IsColliding())
		{
			var hitObject = (Spatial) GetCollider();

			// check if hit object is target
			if(hitObject == target || hitObject.Owner == target)
			{
				return true;
			}
		}

		return false;
	}
}
