using Godot;
using System;

public class MobWimpStateAttack : MobWimpState
{

    // Wimp attacks when close enough to enemy

	float startTime;




    public override void RunState(float delta)
	{		

	}



	public override void StartState()
	{
		blackboard.usePath = false;

		startTime = EngineTime.timePassed;
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