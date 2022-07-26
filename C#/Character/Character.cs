using Godot;
using System;

public class Character : KinematicBody
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateStart,
		stateIdle,
		stateMove,
		stateJump,
		stateFall,
		stateSlide,
		stateJumpPad;

	[Export]
	public float speed = 6,
		slopeSpeed = 3,
		acceleration = 15,
		jumpPadAcceleration = 1f, 
		jumpHeight = 2.25f,
		maxSlopeAngle = 40,
		maxSlideAngle = 70,
		maxFallSpeed = 20;
	[Export]
	NodePath cameraSpringArmPath;
	public float gravity,
		maxSlopeAngleRad,
		maxSlideAngleRad,
		ySpeed,
		jumpStartY;
	public Vector3 velocity,
		jumpPadVelocity,
		snap = Vector3.Down;
	public CameraSpringArm cameraSpringArm;
	public Disconnector jumpDisconnector = new Disconnector();



	public override void _Ready()
	{
		// get gravity
		var gravityVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		var gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
		gravity = gravityVector.y * gravityMagnitude;

		// calculate angles in radians
		maxSlopeAngleRad = Mathf.Pi / 180f * maxSlopeAngle;
		maxSlideAngleRad = Mathf.Pi / 180f * maxSlideAngle;

		// get nodes
		//cameraSpringArm = GetNode<CameraSpringArm>("CameraSpringArm");
		cameraSpringArm = GetNode(cameraSpringArmPath) as CameraSpringArm;

		// initialize states
		stateIdle = new CharacterStateIdle(){blackboard = this};
		stateStart = new CharacterStateStart(){blackboard = this};
		stateMove = new CharacterStateMove(){blackboard = this};
		stateJump = new CharacterStateJump(){blackboard = this};
		stateFall = new CharacterStateFall(){blackboard = this};
		stateSlide = new CharacterStateSlide(){blackboard = this};
		stateJumpPad = new CharacterStateJumpPad(){blackboard = this};

		// set first state in machine
		machine.SetState(stateStart);
	}



	public override void _PhysicsProcess(float delta)
	{
		// run machine
		machine.CurrentState.RunState(delta);
		machine.SetState(machine.CurrentState.Transition());

		// debug   
		//GD.Print(machine.CurrentState.ToString()); 
	}
}
