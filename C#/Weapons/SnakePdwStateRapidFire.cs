using Godot;
using System;

public class SnakePdwStateRapidFire : SnakePdwState
{

    float startTime = -1;



    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;

         // fire
        blackboard.barrelRapidFire.Fire();
        blackboard.casingEjectorPistol.Fire();
        blackboard.muzzleFlashFx.Fire();

        // audio
        var newWeaponAudio = new AudioStreamPlayer3D();
        blackboard.AddChild(newWeaponAudio);
        newWeaponAudio.Stream = blackboard.rapidFireSound;
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
        if(EngineTime.timePassed > startTime + 60 / blackboard.rateOfRapidFire)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}
