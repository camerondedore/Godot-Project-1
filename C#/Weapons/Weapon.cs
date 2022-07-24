using Godot;
using System;

public class Weapon : Spatial
{
    
    [Export]
    NodePath barrelPath;
    [Export]
    float rateOfFire = 600;
    protected TimedDisconnector autoSear = new TimedDisconnector();
    Barrel barrel;



    public override void _Ready()
    {
        // calculate cycle time
        var cycleTime = 60 / rateOfFire;

        // set up auto sear
        autoSear.releaseTime = cycleTime;

        // get barrel
        barrel = GetNode(barrelPath) as Barrel;
    }



    public override void _Process(float delta)
    {
        if(autoSear.Trip(PlayerInput.fire1))
        {
            // fire
            barrel.Fire();
        }
    }
}
