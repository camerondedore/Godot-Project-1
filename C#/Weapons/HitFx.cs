using Godot;
using System;

public class HitFx : Spatial
{
	
	[Export]
	NodePath mainFxPath;
	CPUParticles mainFx;



	public override void _Ready()
	{
		// get nodes
		mainFx = GetNode<CPUParticles>(mainFxPath);

		// play fx
		mainFx.Emitting = true;
	}
}
