using Godot;
using System;

public class HitFx : Spatial
{
	
	[Export]
	NodePath mainFxPath,
		blastFxPath;
	[Export]
	float life = 0.5f,
		blastScaleRange = 0.4f,		
		blastSpread = 0.1f,
		blastLife = 0.1f,
		blastSpeed = 5;
	CPUParticles mainFx;
	Spatial blastFx;
	float startTime,
		blastSpeedVariation;



	public override void _Ready()
	{
		startTime = OS.GetTicksMsec() * 0.001f;

		// get nodes
		mainFx = GetNode<CPUParticles>(mainFxPath);
		blastFx = GetNode<Spatial>(blastFxPath);

		// play particle fx
		mainFx.Emitting = true;


		// set up blast fx scale variation
		blastSpeedVariation = (GD.Randf() - 0.5f) * 2 * blastScaleRange;

		// set blast fx scale to zero
		blastFx.Scale = Vector3.Zero;

		// get new scale
		//var scaleForBlast = 1 - (GD.Randf() - 0.5f) * 2 * blastScaleRange;
		
		// set new scale
		// spatial had the Scale property for getting and setting
		//blastFx.Scale = (blastFx.Scale * scaleForBlast);

		// get new forward direction
		var forwardForBlast = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f) * blastSpread;
		
		// get new up direction
		var upForBlast = new Vector3(GD.Randf() - 0.5f, GD.Randf() - 0.5f, GD.Randf() - 0.5f);

		// set position and new forward direction
		blastFx.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -1 + forwardForBlast, upForBlast);
	}



	public override void _Process(float delta)
	{
		var currentTime = OS.GetTicksMsec() * 0.001f;
		// check life time
		if(startTime + life < currentTime)
		{
			// destroy
			QueueFree();
		}

		// check blast fx life time
		if(startTime + blastLife < currentTime)
		{
			// hide
			blastFx.Hide();
		}

		// scale blast fx
		blastFx.Scale = (blastFx.Scale + Vector3.One * blastSpeed * delta);
	}	
}
