using Godot;
using System;

public class Pause : Node
{
    
    Disconnector discon = new Disconnector();



    public override void _Ready()
    {
        // lock cursor
        Input.SetMouseMode(Input.MouseMode.Captured);
    }



    public override void _Process(float delta)
    {
        if(discon.Trip(PlayerInput.pause))
        {
            if(Engine.TimeScale > 0)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }        
    }



    public static void ResumeGame()
    {
        Engine.TimeScale = 1;
                
        // lock cursor
        Input.SetMouseMode(Input.MouseMode.Captured);
    }



    public static void PauseGame()
    {
        Engine.TimeScale = 0;
                
        // lock cursor
        Input.SetMouseMode(Input.MouseMode.Visible);
    }
}
