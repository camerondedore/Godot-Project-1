using Godot;
using System;

public class MobEyes : RayCast
{
	
	[Export]
	float viewRange = 25;
	//uint mask = 1;

	float viewRangeSquared;

   
	public override void _Ready()
	{
		viewRangeSquared = viewRange * viewRange;
	}



	public bool CanSeeTarget(Spatial target)
	{
		// check distance
		if(target.GlobalTransform.origin.DistanceSquaredTo(GlobalTransform.origin) > viewRangeSquared)
		{
			// too far
			return false;
		}

		// get target position in local space
		CastTo = target.GlobalTransform.origin - GlobalTransform.origin; 

		// cast ray
		ForceRaycastUpdate();

		// check collision
		if(IsColliding())
		{
			var hitObject = (Spatial) GetCollider();
			
			// check if hit object is target
			if(hitObject.Owner == target.Owner)
			{
				return true;
			}
		}

		return false;
	}
}
