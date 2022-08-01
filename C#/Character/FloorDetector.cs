using Godot;
using System;

public class FloorDetector : Node
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

			while(collisionCount > 0)
			{
				collisionCount--;
				var node = (Node) blackboard.GetSlideCollision(0).Collider;
				var nodeGroups = node.GetGroups();

				// check groups
				if(nodeGroups.Contains("jump pad"))
				{
					// use jump pad
					var jumpPad = node as JumpPad;
						
					// set character jump pad velocity
					blackboard.jumpPadVelocity = jumpPad.GetVelocity(blackboard.gravity, blackboard.GlobalTransform.origin);
						
					// set character state to jump pad
					blackboard.machine.SetState(blackboard.stateJumpPad);

					// play animation
					jumpPad.PlayAnimation();
				}
				else if(nodeGroups.Contains("slippery"))
				{
					if(!(blackboard.machine.CurrentState is CharacterStateSlide))
					{
						// set character state to slide
						blackboard.machine.SetState(blackboard.stateSlide);
					}
				}
			}
		}
	}
}
