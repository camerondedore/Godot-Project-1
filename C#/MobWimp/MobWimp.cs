using Godot;
using System;

public class MobWimp : KinematicBody
{
	
	// debug
	string lastdebug = "";
	public StateMachineQueue machine = new StateMachineQueue();
	public State stateIdle,
		stateDie,
		statePursue,
		stateAttack,
		stateSearch;

	[Export]
	public float speed = 7,
		meleeDamage = 10,
		meleeDamageRangeSquared = 3,
		attackRangeSquared = 1,
		viewRangeSquared = 225,
		yMinPursueDistance = 1.25f,
		attackTime = 1,
		damageTime = 0.5f;

	[Export]
	NodePath mobEyesNodePath,
		enemyNodePath;

	public Spatial enemy;
	public MobEyes eyes;
	public MobKinematicBody body;
	public Godot.WeakRef enemyRef;



	public override void _Ready()
	{
		// initialize body
		body = new MobKinematicBody(){myBody = this};

		// get nodes
		eyes = GetNode<MobEyes>(mobEyesNodePath);
		enemy = GetNode<Spatial>(enemyNodePath);

		// get enemy ref
		enemyRef = Godot.Object.WeakRef(enemy);
		
		// initialize states
		stateIdle = new MobWimpStateIdle(){blackboard = this};
		stateDie = new MobWimpStateDie(){blackboard = this};
		statePursue = new MobWimpStatePursue(){blackboard = this};
		stateAttack = new MobWimpStateAttack(){blackboard = this};
		stateSearch = new MobWimpStateSearch(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);

		// add queued state machine to popper
		machine.Enable();
	}



	public override void _Process(float delta)
	{
		// debug
		// if(machine.CurrentState.ToString() != lastdebug)
		// {
		// 	GD.Print(machine.CurrentState);
		// 	lastdebug = machine.CurrentState.ToString();
		// 	//GD.Print("Path length: " + path.Length);
		// 	//GD.Print("Can see enemy: " + eyes.CanSeeTarget(enemy).ToString());
		// }

		// check for enemy
		if(enemyRef.GetRef() == null)
		{
			// remove machine from queue
			machine.Disable();
		}
	}



	public override void _PhysicsProcess(float delta)
	{
		// run body
		body.Run(delta);
	}
}
