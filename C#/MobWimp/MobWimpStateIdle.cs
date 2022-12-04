using Godot;
using System;

public class MobWimpStateIdle : MobWimpState
{

	// Wimp idles when he has no route to his enemy





    public override void RunState(float delta)
	{		
        //blackboard.eyes.CanSeeTarget(blackboard.enemy);

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
		//blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check if mob has path to enemy
		if(blackboard.path.Length > 0)
		{
			// seek
			return blackboard.stateSeek;
		}

		return this;
	}
}