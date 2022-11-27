using Godot;
using System;

public class MobWimpHealth : Health
{
    
    [Export]
    NodePath mobWimpNodePath;

    MobWimp mobWimp;


    public override void _Ready()
    {
        // get nodes
        mobWimp = GetNode<MobWimp>(mobWimpNodePath);
    }



    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        // add hurt state transition here
    }



    public override void Die()
    {
        mobWimp.machine.SetState(mobWimp.stateDie);
    }
}
