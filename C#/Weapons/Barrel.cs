using Godot;
using System;

public class Barrel : Spatial
{
    
    [Export]
    PackedScene bulletScene;
    [Export]
    float spread = 1;
    [Export]
    int shotCount = 1;



    public override void _Ready()
    {
        //GD.Randomize();
    }



    public void Fire()
    {
        var c = shotCount;

        // create each shot
        while(c > 0)
        {
            c--;

            // load bullet resource
            var bullet = bulletScene.Instance() as Bullet;

            // get spread
            var spreadForShot = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f) * spread;

            // GlobalTransform only works when parents are Spatial
            // set bullet position
            bullet.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -100 + spreadForShot, Vector3.Up);
            
            // set parent as root scene
            GetTree().Root.AddChild(bullet);
        }
    }
}
