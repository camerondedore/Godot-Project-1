using Godot;
using System;

public class MobWimpStateSearch : MobWimpState
{

    // Wimp searches when there is no LOS to enemy




    public override void RunState(float delta)
	{		
		// check if last path is complete
        if(blackboard.body.pathIndex == blackboard.body.path.Length)
		{
			// regenerate get path to enemy
			var randomDestinationModifier = new Vector3(GD.Randf() - 0.5f, 0, GD.Randf() - 0.5f) * 4; // random radius of 2
			blackboard.body.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin + randomDestinationModifier, true);

			blackboard.body.pathIndex = 0;
		}
	}



	public override void StartState()
	{
		// get path to enemy
		blackboard.body.path = MobPathing.navNode.GetSimplePath(blackboard.GlobalTransform.origin, blackboard.enemy.GlobalTransform.origin, true);

		blackboard.body.pathIndex = 0;
		blackboard.body.usePath = true;
	}



	public override void EndState()
	{
		blackboard.body.usePath = false;
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