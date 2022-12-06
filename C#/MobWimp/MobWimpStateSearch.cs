using Godot;
using System;

public class MobWimpStateSearch : MobWimpState
{

    // Wimp searches when there is no LOS to enemy




    public override void RunState(float delta)
	{		

	}



	public override void StartState()
	{
		// get path to enemy
		blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin, true);

		blackboard.pathIndex = 0;

		blackboard.usePath = true;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		// LOS check
		var canSeeEnemy = blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check for no LOS to enemy
		if(canSeeEnemy)
		{
			// pursue
			return blackboard.statePursue;
		}

		return this;
	}
}