using Godot;
using System;

public class MobWimpStatePursue : MobWimpState
{

    // Wimp seeks when there is LOS to enemy




    public override void RunState(float delta)
	{		
		// check if last path is complete
        if(blackboard.pathIndex == blackboard.path.Length)
		{
			// regenerate get path to enemy
			var randomDestinationModifier = new Vector3(GD.Randf() - 0.5f, 0, GD.Randf() - 0.5f) * 4; // random radius of 2
			blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin + randomDestinationModifier, true);

			blackboard.pathIndex = 0;
		}
	}



	public override void StartState()
	{
		// get path to enemy
		var randomDestinationModifier = new Vector3(GD.Randf() - 0.5f, 0, GD.Randf() - 0.5f) * 4; // random radius of 2
		blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin + randomDestinationModifier, true);

		blackboard.pathIndex = 0;

		blackboard.usePath = true;
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

		// check for LOS to enemy and distance to enemy
		if(canSeeEnemy && distanceToEnemySquared < blackboard.attackRangeSquared)
		{
			// attack
			return blackboard.stateAttack;
		}

		

		return this;
	}
}