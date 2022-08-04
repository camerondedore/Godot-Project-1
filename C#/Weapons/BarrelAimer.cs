using Godot;
using System;

public class BarrelAimer : Spatial
{
    
    [Export]
    NodePath barrelPath,
        cameraSpringArmPath;
    [Export]
    float rangeMax = 50,
        rangeMin = 5;
    [Export]
    uint mask = 1;
    Spatial barrel;
    CameraSpringArm cameraSpringArm;
    PhysicsDirectSpaceState spaceState;




    public override void _Ready()
    {
        // get barrel
        barrel = GetNode<Spatial>(barrelPath);
        
        // get camera spring arm
        cameraSpringArm = GetNode<CameraSpringArm>(cameraSpringArmPath);


        // get physics state
		// only works in _PhysicsProcess
		spaceState = GetWorld().DirectSpaceState;
    }



    public override void _Process(float delta)
    {
        // get ray values
        var rayStartPosition = cameraSpringArm.GlobalTransform.origin;
        var rayMinPosition = rayStartPosition + cameraSpringArm.GlobalTransform.basis.z * -1 * rangeMin;
        var rayMaxPosition = rayStartPosition + cameraSpringArm.GlobalTransform.basis.z * -1 * rangeMax;

        // set default value for target position to the range max
        var targetPoint = rayMaxPosition;

        // cast ray from camera
        var rayResults = spaceState.IntersectRay(rayStartPosition, rayMaxPosition, new Godot.Collections.Array { this, Owner }, mask);

        if(rayResults.Contains("collider"))
        {
            var hitPoint = (Vector3) rayResults["position"];
            var distanceToHitSqr = rayStartPosition.DistanceSquaredTo(hitPoint);
            
            if(distanceToHitSqr < rangeMin * rangeMin)
            {
                // too close so use rayMinPosition
                targetPoint = rayMinPosition;
            }
            else
            {
                // use ray hit point
                targetPoint = (Vector3) rayResults["position"];
            }
        }

        // aim barrel
        barrel.LookAtFromPosition(barrel.GlobalTransform.origin, targetPoint, Vector3.Up);
    }
}
