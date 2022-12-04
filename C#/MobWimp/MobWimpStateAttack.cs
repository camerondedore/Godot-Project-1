using Godot;
using System;

public class MobWimpStateAttack : MobWimpState
{

    // Wimp attacks when close enough to enemy and enemy is visible




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
		// check if there is no path to enemy
        // if(blackboard.path.Length == 0)
		// {
		// 	// idle
		// 	return blackboard.stateIdle;
		// }

		// distance check to enemy
		var wimpPosition = blackboard.GlobalTransform.origin;
		var enemyPosition = blackboard.enemy.GlobalTransform.origin;
		var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

		var withinAttackRange = distanceToEnemySquared > blackboard.attackMinRangeSquared && distanceToEnemySquared > blackboard.attackMinRangeSquared;

		// LOS check
		var canSeeEnemy = blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check for being enemy too far away or wimp has no LOS to enemy
		if(distanceToEnemySquared > blackboard.attackMaxRangeSquared || !canSeeEnemy)
		{
			// seek
			return blackboard.stateSeek;
		}

		// check for enemy being too close and can see enemy
		// if(distanceToEnemySquared < blackboard.attackMaxRangeSquared && canSeeEnemy)
		// {
		// 	// retreat
		// 	return blackboard.stateRetreat;
		// }

		return this;
	}
}