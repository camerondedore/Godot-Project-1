using Godot;
using System;

public class CharacterUi : Node
{
    
    [Export]
    NodePath uiRootPath;
    Control uiRoot;




    public override void _Ready()
    {       
        // get nodes
        uiRoot = GetNode<Control>(uiRootPath);
    }



    public override void _Process(float delta)
    {
         if(uiRoot.Visible == true && Engine.TimeScale == 0)
        {
            // disable ui
            uiRoot.Visible = false;
        }

        if(uiRoot.Visible == false && Engine.TimeScale != 0)
        {
            // enable ui
            uiRoot.Visible = true;
        }
    }
}
