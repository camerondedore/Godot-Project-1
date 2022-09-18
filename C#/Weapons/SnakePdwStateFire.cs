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
        startTime = EngineTime.timePassed;

         // fire
        blackboard.barrel.Fire();
        blackboard.casingEjectorPistol.Fire();
        blackboard.muzzleFlashFx.Fire();

        // audio
        var newWeaponAudio = new AudioStreamPlayer3D();
        blackboard.AddChild(newWeaponAudio);
        newWeaponAudio.Stream = blackboard.fireSound;
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
        // check if landing from great height
        if(blackboard.character.machine.CurrentState is CharacterStateLandHard)
        {
            // interrupt fire
            // scatter shot
            return blackboard.stateScatterShot;
        }
        
        if(EngineTime.timePassed > startTime + 60 / blackboard.rateOfFire)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}
