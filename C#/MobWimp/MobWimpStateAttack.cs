using Godot;
using System;

public class MobWimpStateAttack : MobWimpState
{

    // Wimp attacks when close enough to enemy

	float startTime;
	bool didDamage = false;




    public override void RunState(float delta)
	{		
		// check for enemy
		if(blackboard.enemyRef.GetRef() == null)
		{
			// no enemy
			return;
		}
		
		if(!didDamage && startTime + blackboard.damageTime < EngineTime.timePassed)
		{
			// get distance to enemy
			var wimpPosition = blackboard.GlobalTransform.origin;
			var enemyPosition = blackboard.enemy.GlobalTransform.origin;
			var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

			// check distance to enemy
			if(distanceToEnemySquared < blackboard.meleeDamageRangeSquared)
			{
				// damage
				// enemy needs the node "Health" as a direct child
				blackboard.enemy.GetNode<Health>("Health").Damage(blackboard.meleeDamage);
			}
			
			// flip flag even if enemy is out of range
			didDamage = true;
		}
	}



	public override void StartState()
	{
		startTime = EngineTime.timePassed;
		didDamage = false;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		// check for enemy being too close and can see enemy and enough time has passed in attack state
		if(startTime + blackboard.attackTime < EngineTime.timePassed)
		{
			// pursue
			return blackboard.statePursue;
		}

		return this;
	}
}