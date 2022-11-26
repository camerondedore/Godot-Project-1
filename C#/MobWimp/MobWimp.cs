using Godot;
using System;

public class MobWimp : KinematicBody
{
    
    public StateMachine machine = new StateMachine();
    //public State stateStart;



    public override void _Ready()
    {
        // get nodes
		//cameraSpringArm = GetNode<CameraSpringArm>("CameraSpringArm");
        
        // initialize states
		//stateIdle = new CharacterStateIdle(){blackboard = this};

		// set first state in machine
		//machine.SetState(stateStart);
    }



    public override void _Process(float delta)
    {
        
    }
}
