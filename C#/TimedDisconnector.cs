using System.Collections;
using System.Collections.Generic;
using Godot;

public class TimedDisconnector
{

	public float releaseTime = 1;
	float hookTime = -1000;



	/// <summary>
	/// Trips the disconnector.
	/// </summary>
	public void Trip()
	{
		if (OS.GetTicksMsec() * 0.001f > hookTime + releaseTime)
		{
			hookTime = OS.GetTicksMsec() * 0.001f;
		}
	}



	/// <summary>
	/// Returns if an action can happen.
	/// </summary>
	public bool CanTrip()
	{
		return OS.GetTicksMsec() * 1000 > hookTime + releaseTime;
	}
}
