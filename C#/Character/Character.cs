using Godot;
using System;

public class Character : KinematicBody
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateStart,
		stateIdle,
		stateMove,
		stateJumpStart,
		stateJump,
		stateFall,
		stateLand,
		stateLandHard,
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
		landTime = 0.1f,
		landHardTime = 0.3f,
		landHeight = 3,
		jumpStartTime = 0.1f;
	[Export]
	NodePath cameraSpringArmPath,
		characterUiPath;
	public float gravity,
		maxSlopeAngleRad,
		maxSlideAngleRad,
		ySpeed,
		jumpStartY,
		fallStartY;
	public Vector3 velocity,
		jumpPadVelocity,
		snap = Vector3.Down;
	public CameraSpringArm cameraSpringArm;
	public CharacterUi characterUi;
	public Disconnector jumpDisconnector = new Disconnector();

	//string debugText;



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
		cameraSpringArm = GetNode<CameraSpringArm>(cameraSpringArmPath);
		characterUi = GetNode<CharacterUi>(characterUiPath);

		// initialize states
		stateIdle = new CharacterStateIdle(){blackboard = this};
		stateStart = new CharacterStateStart(){blackboard = this};
		stateMove = new CharacterStateMove(){blackboard = this};
		stateJumpStart = new CharacterStateJumpStart(){blackboard = this};
		stateJump = new CharacterStateJump(){blackboard = this};
		stateFall = new CharacterStateFall(){blackboard = this};
		stateLand = new CharacterStateLand(){blackboard = this};
		stateLandHard = new CharacterStateLandHard(){blackboard = this};
		stateSlide = new CharacterStateSlide(){blackboard = this};
		stateJumpPad = new CharacterStateJumpPad(){blackboard = this};

		// set first state in machine
		machine.SetState(stateStart);
	}



	public override void _PhysicsProcess(float delta)
	{
		// run machine
		if(machine != null && machine.CurrentState != null)
		{
			machine.CurrentState.RunState(delta);
			machine.SetState(machine.CurrentState.Transition());
		}

		// debug
		// if(debugText != machine.CurrentState.ToString())
		// {
		// 	GD.Print(machine.CurrentState.ToString());
		// 	debugText = machine.CurrentState.ToString();
		// }   
		//GD.Print(velocity.Length());
		//GD.Print(jumpStartY + " : " + fallStartY); 
	}
}
