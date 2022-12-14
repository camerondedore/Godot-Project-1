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
    Vector3 lastPosition,
        velocityDelta,
        velocity;
    


    
    public override void _Ready()
    {
        lastPosition = GlobalTransform.origin;
    }



    public override void _PhysicsProcess(float delta)
    {
        // get velocity for inertia
        velocityDelta = GlobalTransform.origin - lastPosition;
        velocity = velocityDelta / delta;
        lastPosition = GlobalTransform.origin;
    }



    public void Fire()
    {
        // load casing resource
        var casing = casingScene.Instance() as RigidBody;

        // get spread
        var spreadForShot = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f) * spread;
        // get ejection direction
        var ejectionDirection = direction + spreadForShot;
        ejectionDirection = this.ToGlobal(ejectionDirection) - GlobalTransform.origin;

        // set bullet position
        casing.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -100, Vector3.Up);

        // eject rigid body
        casing.LinearVelocity = ejectionDirection + velocity;
        casing.AngularVelocity = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f) * 3.14f * 15;

        // set parent as root scene
        GetTree().Root.AddChild(casing);
    }
}
