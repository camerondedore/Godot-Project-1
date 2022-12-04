using Godot;
using System;

public class MobWimpStateSeek : MobWimpState
{

    // Wimp seeks when there is a path to enemy 
	//  or when wimp can't see enemy




    public override void RunState(float delta)
	{		
        
	}



	public override void StartState()
	{
		// get path to enemy
		blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin, false);
		blackboard.pathIndex = 0;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		// check if there is no path to enemy
        if(blackboard.path.Length == 0)
		{
			// idle
			return blackboard.stateIdle;
		}

		// distance check to enemy
		var wimpPosition = blackboard.GlobalTransform.origin;
		var enemyPosition = blackboard.enemy.GlobalTransform.origin;
		var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

		var withinAttackRange = distanceToEnemySquared > blackboard.attackMinRangeSquared && distanceToEnemySquared > blackboard.attackMinRangeSquared;

		// LOS check
		var canSeeEnemy = blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check for LOS to enemy and distance to enemy
		if(canSeeEnemy && withinAttackRange)
		{
			// attack
			return blackboard.stateAttack;
		}

		

		return this;
	}
}