using Godot;
using System;

public class PauseMenu : Node
{
   
    [Export]
    NodePath menuPath,
        resumeButtonPath,
        quitButtonPath;
    Control menu;
    Button resumeButton,
        quitButton;



    public override void _Ready()
    {
        // get nodes
        menu = GetNode<Control>(menuPath);
        resumeButton = GetNode<Button>(resumeButtonPath);
        quitButton = GetNode<Button>(quitButtonPath);

        // set up buttons
        resumeButton.Connect("pressed", this, "Resume");
        quitButton.Connect("pressed", this, "Quit");
    }



    public override void _Process(float delta)
    {
        if(menu.Visible == false && Engine.TimeScale == 0)
        {
            // enable menu
            menu.Visible = true;
        }

        if(menu.Visible == true && Engine.TimeScale != 0)
        {
            // disable menu
            menu.Visible = false;
        }
    }



    void Resume()
    {
        Pause.ResumeGame();
    }



    void Quit()
    {
        GetTree().Quit();
    }
}
