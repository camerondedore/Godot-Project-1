using Godot;
using System;

public class MuzzleFlashFx : Spatial
{
	
	[Export]
	float lifeTime = 0.1f,
		startScale = 0.5f,
		endScale = 1,
		scaleRange = 0.3f;
	float startTime,
		randomScale;



	public override void _Ready()
	{
		// hide muzzle flash
		Hide();
	}



	public override void _Process(float delta)
	{
		if(Visible)
		{
			var currentTime = OS.GetTicksMsec() * 0.001f;

			// scale up animation
			Scale = Vector3.One * Mathf.Lerp(startScale, endScale * randomScale, (currentTime - startTime) / lifeTime);

			if(startTime + lifeTime < currentTime)
			{
				// end animation
				Hide();
			}
		}
	}



	public void Fire()
	{
		Show();
		startTime = OS.GetTicksMsec() * 0.001f;
		Scale = Vector3.One * startScale;
		randomScale = (GD.Randf() - 0.5f) * scaleRange + 1;
	}
}
