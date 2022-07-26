using Godot;
using System;

public class JumpPad : Area
{
	
	[Export]
	public NodePath targetNodePath;
	[Export]
	public float speed = 10;
	Spatial targetNode;
	Vector3 horizontalVectorToTarget;
	float timeToTarget;



	public override void _Ready()
	{
		// get target node
		targetNode = GetNode(targetNodePath) as Spatial;
		
		// get horizontal vector to target
		var vectorToTarget = targetNode.GlobalTransform.origin - GlobalTransform.origin;
		horizontalVectorToTarget = vectorToTarget;
		horizontalVectorToTarget.y = 0;

		// get time to target
		timeToTarget = horizontalVectorToTarget.Length() / speed;
	}



	public Vector3 GetVelocity(float g)
	{
		// get horizontal velocity
		var velocity = horizontalVectorToTarget.Normalized() * speed;
		
		// get vertical velocity
		// vertical velocity = -0.5 * g * t + (B - A) / t
		velocity.y = -0.5f * g * timeToTarget + (targetNode.GlobalTransform.origin.y - GlobalTransform.origin.y) / timeToTarget;

		return velocity;
	}
}
