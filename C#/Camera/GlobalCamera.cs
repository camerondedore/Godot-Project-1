using Godot;
using System;

public class GlobalCamera : Camera
{

    public StateMachine machine = new StateMachine(); 
	public State stateStart,
        stateTransition,
        stateFollow;
    public static GlobalCamera camera;
    public static Vector3 targetPosition,
        targetLookPoint;
    [Export]
    public float smoothSpeed = 3;
    public float targetSmoothSpeed;



    public override void _Ready()
    {
        // set static reference
        camera = this;

        // initialize states
		stateStart = new GlobalCameraStateStart(){blackboard = this};
		stateTransition = new GlobalCameraStateTransition(){blackboard = this};
		stateFollow = new GlobalCameraStateFollow(){blackboard = this};


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
    }
}
