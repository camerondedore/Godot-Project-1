using Godot;
using System;

public class MobKinematicBody : KinematicBody
{
    
    [Export]
	public float speed = 6,
		slopeSpeed = 3,
		acceleration = 15,
		jumpHeight = 2.25f,
		maxSlopeAngle = 40;

    public Vector3 velocity,
		snap = Vector3.Down;
        
    float gravity,
		maxSlopeAngleRad,
		ySpeed;



    public override void _Ready()
    {
        // get gravity
		var gravityVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		var gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
		gravity = gravityVector.y * gravityMagnitude;

		// calculate angles in radians
		maxSlopeAngleRad = Mathf.Pi / 180f * maxSlopeAngle;
    }



    public override void _PhysicsProcess(float delta)
    {
        
    }
}
