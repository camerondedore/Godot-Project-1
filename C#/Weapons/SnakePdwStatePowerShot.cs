using Godot;
using System;

public class SnakePdwStatePowerShot : SnakePdwState
{

    float startTime = -1;



    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;

         // fire
        blackboard.barrelPowerShot.Fire();
        blackboard.casingEjectorRifle.Fire();
        blackboard.muzzleFlashFx.Fire();

        // audio
        var newWeaponAudio = new AudioStreamPlayer3D();
        blackboard.AddChild(newWeaponAudio);
        newWeaponAudio.Stream = blackboard.powerShotSound;
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
        if(EngineTime.timePassed > startTime + blackboard.powerShotTime)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}
