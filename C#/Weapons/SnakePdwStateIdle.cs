using Godot;
using System;

public class SnakePdwStateIdle : SnakePdwState
{





    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
        
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        if(blackboard.trigger > 0)
        {
            // check if sliding
            if(blackboard.character.machine.CurrentState is CharacterStateSlide && 
                blackboard.character.slideTime > blackboard.rapidFireThreshold)
            {
                // rapid fire
                return blackboard.stateRapidFire;
            }

            // check if using jump pad
            if(blackboard.character.machine.CurrentState is CharacterStateJumpPad)
            {
                // power shot
                return blackboard.statePowerShot;
            }

            // check if landing from great height
            if(blackboard.character.machine.CurrentState is CharacterStateLandHard)
            {
                // scatter shot
                return blackboard.stateScatterShot;
            }

            // fire
            return blackboard.stateFire;
        }

        return this;
    }
}
