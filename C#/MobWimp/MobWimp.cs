using Godot;
using System;

public class MobWimp : KinematicBody
{
	
	public StateMachineQueue machine = new StateMachineQueue();
	public State stateIdle,
		stateDie,
		stateSeek,
		stateAttack,
		stateRetreat;

	[Export]
	public float speed = 7;

	[Export]
	NodePath mobEyesNodePath,
		enemyNodePath;


	public Spatial enemy;
	public MobEyes eyes;
	public Vector3[] path;
	int pathIndex = 0;
	public Vector3 targetVelocity;



	public override void _Ready()
	{
		// get nodes
		eyes = GetNode<MobEyes>(mobEyesNodePath);
		enemy = GetNode<Spatial>(enemyNodePath);
		
		// initialize states
		stateIdle = new MobWimpStateIdle(){blackboard = this};
		stateDie = new MobWimpStateDie(){blackboard = this};
		stateSeek = new MobWimpStateSeek(){blackboard = this};
		stateAttack = new MobWimpStateAttack(){blackboard = this};
		stateRetreat = new MobWimpStateRetreat(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);

		// add queued state machine to popper
		machine.Enable();
	}



	public override void _Process(float delta)
	{
		if(path.Length > 0 && pathIndex < path.Length)
		{
			// ...
			MoveAndSlideWithSnap(targetVelocity, Vector3.Down, Vector3.Up, true, 4, 1f);
		}
	}
}
