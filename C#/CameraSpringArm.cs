using Godot;
using System;

public class CameraSpringArm : SpringArm
{

	public bool freezeY = false;
	[Export]
	NodePath cameraTargetPath;
	[Export]
	float sensitivity = 0.15f,
		minAngle = -50,
		maxAngle = 40,
		smoothSpeed = 8;
	Spatial cameraTarget;
	Vector3 offset,
		targetPosition;
	float y;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// get initial values
		offset = Translation;
		targetPosition = GlobalTransform.origin;

		// get target for camera
		cameraTarget = GetNode(cameraTargetPath) as Spatial;

		SetAsToplevel(true);

		// lock cursor
		Input.SetMouseMode(Input.MouseMode.Captured);
	}



	public override void _UnhandledInput(InputEvent e)
	{	
		if(e is InputEventMouseMotion)
		{
			var mouseDirection = RotationDegrees;
			mouseDirection.x -= PlayerInput.look.y * sensitivity;
			mouseDirection.x = Mathf.Clamp(mouseDirection.x, minAngle, maxAngle);
			mouseDirection.y -= PlayerInput.look.x * sensitivity;
			mouseDirection.y = Mathf.Wrap(mouseDirection.y, 0, 360);

			RotationDegrees = mouseDirection;
		}
	}



	public void MoveToFollowCharacter(Vector3 characterPosition)
	{
		// get position for spring arm
		var newPositon = characterPosition + offset;

		if(!freezeY)
		{
			// set y to match character
			y = newPositon.y;
		}

		// apply y to new position
		var newPositonWithY = newPositon;
		newPositonWithY.y = y;

		// move to follow
		targetPosition = newPositonWithY;
	}



	public override void _PhysicsProcess(float delta)
	{
		var smoothPosition = targetPosition;
		smoothPosition.y = Mathf.Lerp(GlobalTransform.origin.y, targetPosition.y, smoothSpeed * delta);
		Translation = smoothPosition;

		// move camera
		GlobalCamera.targetPosition = cameraTarget.GlobalTransform.origin;
		GlobalCamera.targetLookPoint = cameraTarget.GlobalTransform.origin + cameraTarget.GlobalTransform.basis.z * -100;
	}
}
