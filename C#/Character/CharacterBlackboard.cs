using Godot;
using System;

public class CharacterBlackboard : KinematicBody
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateIdle,
		stateMove,
		stateJump,
		stateFall,
		stateSlide;

	[Export]
	public float speed = 6,
		slopeSteeringSpeed = 6,
		acceleration = 15, 
		jumpHeight = 2,
		gravity = -9.81f,
		maxSlopeAngle = 40;
	public float maxSlopeAngleRad;
	public Vector3 velocity,
		snap = Vector3.Down;
	public SpringArm springArm;
	public Spatial mesh;
	public Disconnector jumpDisconnector = new Disconnector();



	public override void _Ready()
	{
		// calculate slope in radians
		maxSlopeAngleRad = Mathf.Pi / 180f * maxSlopeAngle;

		// get nodes
		springArm = GetNode<SpringArm>("SpringArm");
		mesh = GetNode<Spatial>("mesh");

		// initialize states
		stateIdle = new CharacterStateIdle(){blackboard = this};
		stateMove = new CharacterStateMove(){blackboard = this};
		stateJump = new CharacterStateJump(){blackboard = this};
		stateFall = new CharacterStateFall(){blackboard = this};
		stateSlide = new CharacterStateSlide(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);
	}



	public override void _Process(float delta)
	{
		// run machine
		machine.CurrentState.RunState(delta);
		machine.SetState(machine.CurrentState.Transition());

		// debug   
		//GD.Print(machine.CurrentState.ToString()); 
	}
}
