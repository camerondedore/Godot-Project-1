using Godot;
using System;

public class MobWimpStatePursue : MobWimpState
{

    // Wimp seeks when there is LOS to enemy




    public override void RunState(float delta)
	{		
		// check for enemy
		if(blackboard.enemyRef.GetRef() == null)
		{
			// no enemy
			return;
		}

		// get direction to enemy
		blackboard.body.targetDirection = (blackboard.enemy.GlobalTransform.origin - blackboard.GlobalTransform.origin).Normalized();
	}



	public override void StartState()
	{

	}



	public override void EndState()
	{
		// clear direction to enemy
		blackboard.body.targetDirection = Vector3.Zero;
	}



	public override State Transition()
	{
		// check for enemy
		if(blackboard.enemyRef.GetRef() == null)
		{
			// no enemy
			return blackboard.stateIdle;
		}

		// get distance to enemy
		var wimpPosition = blackboard.GlobalTransform.origin;
		var enemyPosition = blackboard.enemy.GlobalTransform.origin;
		var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

		// LOS check
		var canSeeEnemy = blackboard.eyes.CanSeeTarget(blackboard.enemy);

		// check for no LOS to enemy
		if(!canSeeEnemy)
		{
			// search
			return blackboard.stateSearch;
		}

		// check distance to enemy
		if(distanceToEnemySquared < blackboard.attackRangeSquared)
		{
			// attack
			return blackboard.stateAttack;
		}
		

		return this;
	}
}