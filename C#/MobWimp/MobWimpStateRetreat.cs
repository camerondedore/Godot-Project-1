using Godot;
using System;

public class MobWimpStateRetreat : MobWimpState
{

    // Wimp retreats when enemy is too close and visible




    public override void RunState(float delta)
	{		

	}



	public override void StartState()
	{
		// get path to enemy
		var randomRetreatMultiplier = blackboard.retreatDistance * (1 + (GD.Randf() - 0.5f) * 0.4f); // +/- 20%
		var retreatDirection = (blackboard.GlobalTransform.origin - blackboard.enemy.GlobalTransform.origin).Normalized() * randomRetreatMultiplier;
		blackboard.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.GlobalTransform.origin + retreatDirection, false);

		blackboard.pathIndex = 0;

		blackboard.usePath = true;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		// distance check to enemy
		var wimpPosition = blackboard.GlobalTransform.origin;
		var enemyPosition = blackboard.enemy.GlobalTransform.origin;
		var distanceToEnemySquared = wimpPosition.DistanceSquaredTo(enemyPosition);

		// check if path is complete or enemy is far enough away
		if(blackboard.pathIndex == blackboard.path.Length || distanceToEnemySquared > blackboard.attackMaxRangeSquared)
		{
			// attack
			return blackboard.stateAttack;
		}

		return this;
	}
}