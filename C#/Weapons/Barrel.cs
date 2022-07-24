using Godot;
using System;

public class Barrel : Spatial
{
    
    [Export]
    PackedScene bulletScene;



    public override void _Ready()
    {

    }



    public void Fire()
    {
        // load bullet resource
        var bullet = bulletScene.Instance() as Bullet;

        // GlobalTransform only works when parents are Spatial

        // set bullet position
        bullet.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -1, Vector3.Up);
        
        // set parent as root scene
        GetTree().Root.AddChild(bullet);
    }



    public void Aim(Vector3 TargetPosition)
    {
        LookAtFromPosition(GlobalTransform.origin, TargetPosition, Vector3.Up);
    }
}
