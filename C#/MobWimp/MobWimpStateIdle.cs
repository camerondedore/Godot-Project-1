using Godot;
using System;

public class MobWimpStateIdle : MobWimpState
{

	// Wimp idles when he has not be aggro'ed





    public override void RunState(float delta)
	{		

	}



	public override void StartState()
	{
		
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		// check for enemy
		if(blackboard.enemyRef.GetRef() == null)
		{
			// no enemy
			return this;
		}

		// get distance to enemy
		var wimpPosition = blackboard.GlobalTransform.origin;
		var enemyPosition = blackboard.enemy.GlobalTransform.origin;
		var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

		// LOS check
		var canSeeEnemy = blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check if mob has LOS to enemy
		if(canSeeEnemy && distanceToEnemySquared < blackboard.viewRangeSquared)
		{
			// pursue
			return blackboard.statePursue;
		}

		return this;
	}
}