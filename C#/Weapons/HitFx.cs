using Godot;
using System;

public class HitFx : Spatial
{
	
	[Export]
	NodePath chunksFxPath,
		chunksSmallFxPath,
		dustFxPath,
		blastFxPath,
		flashFxPath,
		hitAudioPath;
	[Export]
	float life = 0.5f,
		blastSpeedRange = 0.4f,		
		blastSpread = 0.1f,
		blastLife = 0.1f,
		blastSpeed = 5;
	[Export]
	AudioStream hitSound;
	Particles chunksFx,
		chunksSmallFx,
		dustFx,
		flashFx;
	Spatial blastFx;
	AudioStreamPlayer3D hitAudio;
	float startTime,
		blastSpeedVariation;



	public override void _Ready()
	{
		startTime = EngineTime.timePassed;

		// get nodes
		chunksFx = GetNode<Particles>(chunksFxPath);
		chunksSmallFx = GetNode<Particles>(chunksSmallFxPath);
		dustFx = GetNode<Particles>(dustFxPath);
		flashFx = GetNode<Particles>(flashFxPath);
		blastFx = GetNode<Spatial>(blastFxPath);
		hitAudio = GetNode<AudioStreamPlayer3D>(hitAudioPath);

		// play particle fx
		chunksFx.Emitting = true;
		chunksSmallFx.Emitting = true;
		dustFx.Emitting = true;
		flashFx.Emitting = true;

		// play sound
		hitAudio.Stream = hitSound;
		hitAudio.PitchScale = (GD.Randf() - 0.5f) * 0.1f + 1;
		hitAudio.Play();

		// set up blast fx scale variation
		blastSpeedVariation = 1 + (GD.Randf() - 0.5f) * 2 * blastSpeedRange;

		// set blast fx scale to zero
		blastFx.Scale = Vector3.One * 0.05f;

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
		var currentTime = EngineTime.timePassed;
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
		blastFx.Scale = (blastFx.Scale + Vector3.One * blastSpeed * blastSpeedVariation * delta);
	}	
}
