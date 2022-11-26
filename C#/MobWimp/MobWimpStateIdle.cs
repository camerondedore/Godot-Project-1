using Godot;
using System;

public class MobWimpStateIdle : MobWimpState
{





    public override void RunState(float delta)
	{		
        //blackboard.eyes.CanSeeTarget();
        GD.Print("run state");
	}



	public override void StartState()
	{
        GD.Print("start state");
	}



	public override void EndState()
	{
        GD.Print("end state");
	}



	public override State Transition()
	{
		return this;
	}
}