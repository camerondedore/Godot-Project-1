using Godot;
using System;

public class MobWimp : KinematicBody
{
	
	public StateMachineQueue machine = new StateMachineQueue();
	public State stateIdle;

	[Export]
	public float speed = 7;

	[Export]
	NodePath mobEyesPath;

	public MobEyes eyes;



	public override void _Ready()
	{
		// get nodes
		eyes = GetNode<MobEyes>(mobEyesPath);
		
		// initialize states
		stateIdle = new MobWimpStateIdle(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);

		// add queued state machine to popper
		machine.Enable();
	}



	// public override void _Process(float delta)
	// {
		
	// }
}
