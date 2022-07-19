using Godot;
using System;

public class SpringArm : Godot.SpringArm
{

	[Export]
	float sensitivity = 1;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
			mouseDirection.x = Mathf.Clamp(mouseDirection.x, -90, 30);
			mouseDirection.y -= PlayerInput.look.x * sensitivity;
			mouseDirection.y = Mathf.Wrap(mouseDirection.y, 0, 360);

			RotationDegrees = mouseDirection;
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
