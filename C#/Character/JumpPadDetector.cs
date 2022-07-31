using Godot;
using System;

public class JumpPadDetector : Node
{
	[Export]
	public NodePath characterPath;
	Character blackboard;



	public override void _Ready()
	{
		// get character blackboard
		blackboard = GetNode(characterPath) as Character;
	}



	public override void _PhysicsProcess(float delta)
	{
		// check for jump pad
		if(blackboard.GetSlideCount() > 0)
		{
			var collisionCount = blackboard.GetSlideCount();
			Node floorNode = null;

			while(collisionCount > 0)
			{
				collisionCount--;
				var node = (Node) blackboard.GetSlideCollision(0).Collider;

				// check groups
				if(node.GetGroups().Contains("jump pad"))
				{
					floorNode = node;
				}
			}
			

			if(floorNode != null)
			{
				// get jump pad
				var jumpPad = floorNode as JumpPad;
					
				// set character jump pad velocity
				blackboard.jumpPadVelocity = jumpPad.GetVelocity(blackboard.gravity, blackboard.GlobalTransform.origin);
					
				// set character state to jump pad
				blackboard.machine.SetState(blackboard.stateJumpPad);

				// play animation
				jumpPad.PlayAnimation();
			}
		}
	}
}
