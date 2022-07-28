using Godot;
using System;

public class SnakePdwStateFire : SnakePdwState
{

    float startTime = -1;



    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
        startTime = OS.GetTicksMsec() * 0.001f;

         // fire
        blackboard.barrel.Fire();

        // audio
        var newWeaponAudio = new AudioStreamPlayer3D();
        blackboard.AddChild(newWeaponAudio);
        newWeaponAudio.Stream = blackboard.fireSound;
        newWeaponAudio.MaxDistance = blackboard.weaponAudio.MaxDistance;
        newWeaponAudio.UnitSize = blackboard.weaponAudio.UnitSize;
        newWeaponAudio.Connect("finished", newWeaponAudio, "queue_free");
        newWeaponAudio.Play();
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        // check if landing from great height
        if(blackboard.character.machine.CurrentState is CharacterStateLandHard)
        {
            // interrupt fire
            // scatter shot
            return blackboard.stateScatterShot;
        }
        
        if(OS.GetTicksMsec() * 0.001f > startTime + 60 / blackboard.rateOfFire)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}