using Godot;
using System;

public class SnakePdwStateScatterShot : SnakePdwState
{

     float startTime = -1;



    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
        startTime = OS.GetTicksMsec() * 0.001f;

         // fire
        blackboard.barrelScatterShot.Fire();

        // audio
        var newWeaponAudio = new AudioStreamPlayer3D();
        blackboard.AddChild(newWeaponAudio);
        newWeaponAudio.Stream = blackboard.scatterShotSound;
        newWeaponAudio.MaxDistance = blackboard.weaponAudioPlayer.MaxDistance;
        newWeaponAudio.UnitSize = blackboard.weaponAudioPlayer.UnitSize;
        newWeaponAudio.Connect("finished", newWeaponAudio, "queue_free");
        newWeaponAudio.Play();
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        if(OS.GetTicksMsec() * 0.001f > startTime + blackboard.scatterShotTime)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}
