using Godot;
using System;

public class UiCrosshair : TextureRect
{
    
    [Export]
    Texture defaultCrosshair,
        rapidCrosshair,
        scatterCrosshair,
        powerCrosshair;
    [Export]
    NodePath characterNodePath;
    Character character;



    public override void _Ready()
    {
        // get node
        character = GetNode<Character>(characterNodePath);

        // set crosshair
        this.Texture = defaultCrosshair;
    }



    public override void _Process(float delta)
    {
        if(character.machine.CurrentState is CharacterStateSlide)
        {
            // show rapid crosshair
            this.Texture = rapidCrosshair;

            return;
        }
        
        if(character.machine.CurrentState is CharacterStateLandHard)
        {
            // show scatter crosshair
            this.Texture = scatterCrosshair;

            return;
        }
        
        if(character.machine.CurrentState is CharacterStateJumpPad)
        {
            // show power crosshair
            this.Texture = powerCrosshair;

            return;
        }
        
        
        // show default crosshair
        this.Texture = defaultCrosshair;        
    }
}
