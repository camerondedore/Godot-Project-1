using Godot;
using System;

public class MobWimpStateDie : MobWimpState
{





    public override void RunState(float delta)
	{		
        //blackboard.eyes.CanSeeTarget(blackboard.enemy);
	}



	public override void StartState()
	{
        // remove state machine from queue
        blackboard.machine.Disable();

        // destroy wimp
        blackboard.QueueFree();
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		return this;
	}
}