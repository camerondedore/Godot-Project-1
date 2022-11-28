using Godot;
using System;

public class MobWimpStateSeek : MobWimpState
{

    // Wimp seeks when enemy is too far away or not visible




    public override void RunState(float delta)
	{		
        // get path to enemy
		blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin, false);
	}



	public override void StartState()
	{

	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
        if(blackboard.path.Length == 0)
		{
			// seek
			return blackboard.stateSeek;
		}

		return this;
	}
}