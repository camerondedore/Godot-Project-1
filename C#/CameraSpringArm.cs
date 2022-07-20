using Godot;
using System;

public class CameraSpringArm : SpringArm
{

	[Export]
	float sensitivity = 1,
		minAngle = -50,
		maxAngle = 40;
	Vector3 offset;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		offset = Translation;

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
		Translation = characterPosition + offset;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
