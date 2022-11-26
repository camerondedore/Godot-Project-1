using Godot;
using System;
using System.Collections.Generic;

public class StateMachineQueuePopper : Node
{

	[Export] 
    int popPerTick = 4;
	public static List<StateMachineQueue> machines = new List<StateMachineQueue>();
	int machineIndex = 0;



    public override void _Process(float delta)
	{
		// time check
		if(Engine.TimeScale == 0)
		{
			return;
		}

		// get count of pops, maxing out at the number of machines
		int c = Mathf.Clamp(popPerTick, 1, machines.Count);

		// run machine for this tick
		while(c > 0)
		{
			//Debug.Log(machines[machineIndex].gameObject.name + " ran " + Time.time + ", as the " + c + " pop.");
			//Debug.Log(machineIndex + ", " + machines.Count);

			// keep index in range of machines
			if(machineIndex < machines.Count)
			{
				// check active state
				//if(machines[machineIndex].isActiveAndEnabled)
				//{
					// run machine
					machines[machineIndex].RunMachine(delta);
				//}
			}

			// next machine index
			if(machineIndex < machines.Count - 1)
			{
				// next machine
				machineIndex++;
			}
			else
			{
				// first machine
				machineIndex = 0;
			}	

			c--;
		}
	
	}
}
