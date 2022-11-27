using Godot;
using System;

public class MobWimpStateIdle : MobWimpState
{





    public override void RunState(float delta)
	{		
        //blackboard.eyes.CanSeeTarget(blackboard.enemy);
	}



	public override void StartState()
	{

	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		return this;
	}
}