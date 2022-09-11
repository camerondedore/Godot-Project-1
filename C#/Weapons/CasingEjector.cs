using Godot;
using System;

public class CasingEjector : Spatial
{

    [Export]
    PackedScene casingScene;
    [Export]
    Vector3 direction;
    [Export]
    float spread = 1;
    


    
    public override void _Ready()
    {
        
    }




    public void Fire()
    {
        // load casing resource
        var casing = casingScene.Instance() as RigidBody;

        // get spread
        var spreadForShot = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f) * spread;

        // set bullet position
        casing.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + direction * -100 + spreadForShot, Vector3.Up);

        // set parent as root scene
        GetTree().Root.AddChild(casing);
    }
}
