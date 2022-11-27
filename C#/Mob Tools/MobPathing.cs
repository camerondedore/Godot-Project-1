using Godot;
using System;

public class MobPathing : Navigation
{
    // attach to scene navigation node
    // mob will use this for path finding
    
    public static Navigation navNode;



    public override void _Ready()
    {
        navNode = this;
    }
}
