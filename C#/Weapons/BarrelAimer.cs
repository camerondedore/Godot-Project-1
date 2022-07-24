using Godot;
using System;

public class BarrelAimer : Spatial
{
    
    [Export]
    NodePath barrelPath,
        cameraSpringArmPath;
    [Export]
    float range = 50;
    Barrel barrel;
    CameraSpringArm cameraSpringArm;




    public override void _Ready()
    {
        // get barrel
        barrel = GetNode(barrelPath) as Barrel;
        
        // get camera spring arm
        cameraSpringArm = GetNode(cameraSpringArmPath) as CameraSpringArm;
    }



    public override void _Process(float delta)
    {
        // get target point
        var targetPoint = cameraSpringArm.GlobalTransform.origin + cameraSpringArm.GlobalTransform.basis.z * -1 * range;

        // aim barrel
        barrel.Aim(targetPoint);
    }
}
