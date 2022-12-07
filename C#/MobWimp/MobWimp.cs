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
		attackRangeSquared = 1,
		viewRangeSquared = 225,
		attackTime = 1;

	[Export]
	NodePath mobEyesNodePath,
		enemyNodePath;


	public Spatial enemy;
	public MobEyes eyes;
	public Vector3[] path;
	public int pathIndex = 0;
	public bool usePath = true;
	public Vector3 targetVelocity,
		targetDirection;

	Vector3 lastPosition;
	int obstructedCount = 0;



	public override void _Ready()
	{
		// get nodes
		eyes = GetNode<MobEyes>(mobEyesNodePath);
		enemy = GetNode<Spatial>(enemyNodePath);
		
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

		if(usePath && path.Length > 0 && pathIndex < path.Length)
		{
			// get velocity
			targetVelocity = GlobalTransform.origin.DirectionTo(path[pathIndex]).Normalized() * speed;

			// move
			MoveAndSlideWithSnap(targetVelocity, Vector3.Down, Vector3.Up, true, 4, 1f);

			// check distance to path point
			if(GlobalTransform.origin.DistanceSquaredTo(path[pathIndex]) < 0.15f)
			{
				// get next path point
				pathIndex++;
			}
			else
			{
				// get look direction
				var lookTarget = GlobalTransform.origin + targetVelocity;
				lookTarget.y = GlobalTransform.origin.y;

				// look
				LookAt(lookTarget, Vector3.Up);
			}

			// check for obstructions
			// if(GlobalTransform.origin.DistanceSquaredTo(lastPosition) / delta < 0.1f)
			// {
			// 	obstructedCount++;

			// 	if(obstructedCount > 10)
			// 	{
			// 		// end path
			// 		pathIndex = path.Length;
			// 	}
			// }
			// else
			// {
			// 	obstructedCount = 0;
			// }

			// lastPosition = GlobalTransform.origin;
		}
		else if(targetDirection != Vector3.Zero)
		{
			// get velocity
			targetVelocity = targetDirection.Normalized() * speed;
			targetVelocity.y = 0;

			// move
			MoveAndSlideWithSnap(targetVelocity, Vector3.Down, Vector3.Up, true, 4, 1f);

			// get look direction
			var lookTarget = GlobalTransform.origin + targetVelocity;
			lookTarget.y = GlobalTransform.origin.y;

			// look
			LookAt(lookTarget, Vector3.Up);
		}
	}
}
