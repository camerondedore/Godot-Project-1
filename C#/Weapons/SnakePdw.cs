using Godot;
using System;

public class SnakePdw : Spatial
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateIdle,
		stateFire,
		stateRapidFire,
		statePowerShot,
		stateScatterShot;
	[Export]
	NodePath barrelPath,
		barrelRapidFirePath,
		barrelPowerShotPath,
		barrelScatterShotPath,
		weaponAudioPath,
		characterNodePath,
		casingEjectorPistolNodePath,
		casingEjectorShotshellNodePath,
		casingEjectorRifleNodePath,
		muzzleFlashFxPath;
	[Export]
	public float rateOfFire = 600,
		rateOfRapidFire = 1000,
		powerShotTime = 1,
		scatterShotTime = 1,
		rapidFireThreshold = 0.15f,
		scatterShotHeight = 3f;
	[Export]
	public AudioStream fireSound,
		rapidFireSound,
		powerShotSound,
		scatterShotSound;
	public Barrel barrel,
		barrelRapidFire,
		barrelPowerShot,
		barrelScatterShot;
	public AudioStreamPlayer3D weaponAudioPlayer;
	public Character character;
	public CasingEjector casingEjectorPistol,
		casingEjectorShotshell,
		casingEjectorRifle;
	public MuzzleFlashFx muzzleFlashFx;
	public float trigger = 0;



	public override void _Ready()
	{
		// get nodes
		barrel = GetNode<Barrel>(barrelPath);
		barrelRapidFire = GetNode<Barrel>(barrelRapidFirePath);
		barrelPowerShot = GetNode<Barrel>(barrelPowerShotPath);
		barrelScatterShot = GetNode<Barrel>(barrelScatterShotPath);
		weaponAudioPlayer = GetNode<AudioStreamPlayer3D>(weaponAudioPath);
		character = GetNode<Character>(characterNodePath);
		casingEjectorPistol = GetNode<CasingEjector>(casingEjectorPistolNodePath);
		casingEjectorShotshell = GetNode<CasingEjector>(casingEjectorShotshellNodePath);
		casingEjectorRifle = GetNode<CasingEjector>(casingEjectorRifleNodePath);
		muzzleFlashFx = GetNode<MuzzleFlashFx>(muzzleFlashFxPath);


		// initialize states
		stateIdle = new SnakePdwStateIdle(){blackboard = this};
		stateFire = new SnakePdwStateFire(){blackboard = this};
		stateRapidFire = new SnakePdwStateRapidFire(){blackboard = this};
		statePowerShot = new SnakePdwStatePowerShot(){blackboard = this};
		stateScatterShot = new SnakePdwStateScatterShot(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);
	}



	public override void _Process(float delta)
	{
		// possible to move to hands?
		trigger = PlayerInput.fire1;


		// run machine
		if(machine != null && machine.CurrentState != null)
		{
			machine.CurrentState.RunState(delta);
			machine.SetState(machine.CurrentState.Transition());
		}
	}
}
