using Godot;
using System;

public class Weapon : Spatial
{
    
    [Export]
    NodePath barrelPath,
        weaponAudioPath;
    [Export]
    float rateOfFire = 600;
    [Export]
    AudioStream fireSound;
    protected TimedDisconnector autoSear = new TimedDisconnector();
    Barrel barrel;
    AudioStreamPlayer3D weaponAudio;



    public override void _Ready()
    {
        // calculate cycle time
        var cycleTime = 60 / rateOfFire;

        // set up auto sear
        autoSear.releaseTime = cycleTime;

        // get barrel
        barrel = GetNode(barrelPath) as Barrel;

        // get weapon audio
        weaponAudio = GetNode(weaponAudioPath) as AudioStreamPlayer3D;
    }



    public override void _Process(float delta)
    {
        GD.Print(GetChildCount());

        if(autoSear.Trip(PlayerInput.fire1))
        {
            // fire
            barrel.Fire();

            // audio
            weaponAudio.Stream = fireSound;
            weaponAudio.Play();
            // var newWeaponAudio = new AudioStreamPlayer3D();
            // AddChild(newWeaponAudio);
            // newWeaponAudio.Stream = fireSound;
            // newWeaponAudio.MaxDistance = weaponAudio.MaxDistance;
            // newWeaponAudio.MaxDb = weaponAudio.MaxDb;
            // newWeaponAudio.Connect("finished", newWeaponAudio, "QueueFree");
            // newWeaponAudio.Play();
        }
    }
}
