using Godot;
using System;

public class CharacterHealth : Health
{
    
    [Export]
    NodePath healthBarNodePath,
        uiAnimPlayerPath;
    TextureProgress healthBar;
    AnimationPlayer uiAnimPlayer;



    public override void _Ready()
    {
        // get nodes
        healthBar = GetNode<TextureProgress>(healthBarNodePath);
        uiAnimPlayer = GetNode<AnimationPlayer>(uiAnimPlayerPath);

        // update health bar
        healthBar.Value = hitPoints;
    }



    public override void Damage(float dmg)
    {
        base.Damage(dmg);
        
        // update health bar
        healthBar.Value = hitPoints;

        // flash screen
        uiAnimPlayer.Play("UIHealthDamageFlash");
    }



    public override void Heal(float hp)
    {
        base.Heal(hp);

        // update health bar
        healthBar.Value = hitPoints;
    }
}
