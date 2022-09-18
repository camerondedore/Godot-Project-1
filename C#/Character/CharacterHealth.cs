using Godot;
using System;

public class CharacterHealth : Health
{
    
    [Export]
    NodePath healthBarNodePath,
        healthDamageFlashPath;
    TextureProgress healthBar;
    ColorRect healthDamageFlash;



    public override void _Ready()
    {
        // get nodes
        healthBar = GetNode<TextureProgress>(healthBarNodePath);
        healthDamageFlash = GetNode<ColorRect>(healthDamageFlashPath);

        // update health bar
        healthBar.Value = hitPoints;
    }



    public override void Damage(float dmg)
    {
        base.Damage(dmg);
        
        // update health bar
        healthBar.Value = hitPoints;
    }



    public override void Heal(float hp)
    {
        base.Heal(hp);

        // update health bar
        healthBar.Value = hitPoints;
    }
}
