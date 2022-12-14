using Godot;
using System;

public class MobKinematicBody
{
    
    [Export]
	public float speed = 6,
		slopeSpeed = 3,
		acceleration = 15,
		jumpHeight = 2.25f,
		maxSlopeAngle = 40;

    public KinematicBody myBody;
    public Vector3 velocity,
        targetVelocity,
        targetDirection,
		snap = Vector3.Down;
    public Vector3[] path;
	public int pathIndex = 0;
	public bool usePath = false;

    float gravity,
		maxSlopeAngleRad,
		ySpeed;



    public MobKinematicBody()
    {
        // get gravity
		var gravityVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		var gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
		gravity = gravityVector.y * gravityMagnitude;

		// calculate angles in radians
		maxSlopeAngleRad = Mathf.Pi / 180f * maxSlopeAngle;
    }



    public void Run(float delta)
    {
        if(usePath && path.Length > 0 && pathIndex < path.Length)
		{
			// get velocity
			targetVelocity = myBody.GlobalTransform.origin.DirectionTo(path[pathIndex]).Normalized() * speed;

			// set up velocity using input
            // velocity.x = Mathf.Lerp(velocity.x, targetVelocity.x, delta * acceleration);
            // velocity.z = Mathf.Lerp(velocity.z, targetVelocity.z, delta * acceleration);
			velocity.x = targetVelocity.x;
			velocity.z = targetVelocity.z;

            // set snap to grab floor
            snap = -myBody.GetFloorNormal();

            // apply gravity into floor
            velocity += gravity * Vector3.Up * delta;

			// move
			velocity = myBody.MoveAndSlideWithSnap(velocity, snap, Vector3.Up, true, 4, maxSlopeAngleRad);

			// check distance to path point
			if(myBody.GlobalTransform.origin.DistanceSquaredTo(path[pathIndex]) < 0.15f)
			{
				// get next path point
				pathIndex++;
			}
			else
			{
				// get look direction
				var lookTarget = myBody.GlobalTransform.origin + targetVelocity;
				lookTarget.y = myBody.GlobalTransform.origin.y;

				if(lookTarget != myBody.GlobalTransform.origin)
				{
					// look
					myBody.LookAt(lookTarget, Vector3.Up);
				}
			}
		}
		else if(targetDirection != Vector3.Zero)
		{
            // set snap to grab floor
            snap = -myBody.GetFloorNormal();

            // set up velocity using input
            velocity.x = Mathf.Lerp(velocity.x, targetDirection.x * speed, delta * acceleration);
            velocity.z = Mathf.Lerp(velocity.z, targetDirection.z * speed, delta * acceleration);


            // apply gravity into floor
            velocity += gravity * Vector3.Up * delta;

            // move
            velocity = myBody.MoveAndSlideWithSnap(velocity, snap, Vector3.Up, true, 4, maxSlopeAngleRad);

			// get look direction
			var lookTarget = myBody.GlobalTransform.origin + targetDirection;
			lookTarget.y = myBody.GlobalTransform.origin.y;

			// look
			myBody.LookAt(lookTarget, Vector3.Up);
		}
    }
}
