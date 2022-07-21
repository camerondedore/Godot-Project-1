using Godot;
using System;

public class JumpPadDetector : Area
{
	[Export]
	public NodePath characterPath;
	CharacterBlackboard blackboard;



	public override void _Ready()
	{
		// get character blackboard
		blackboard = GetNode(characterPath) as CharacterBlackboard;
	}



	// connected using node signals in editor
	public void _on_JumpPadDetector_area_entered(Area a)
	{
		// get jump pad
		var jumpPad = a as JumpPad;
		
		// set character jump pad velocity
		blackboard.jumpPadVelocity = jumpPad.GetVelocity(blackboard.gravity);
		
		// set character state to jump pad
		blackboard.machine.SetState(blackboard.stateJumpPad);
	}
}
