using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{

    private bool forceApplied;

    private Attack currentAttack;

    
    // passes in the ID of the desired attack animation to be played
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.WeaponDamage.SetAttack(currentAttack.Damage, currentAttack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration); // plays the animation using the name set in the inspector and a fade time
    }

    public override void Tick(float deltaTime)
    {

        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalisedTime(stateMachine.Animator);

        // if we have finished the animation 
        if(normalizedTime < 1f) 
        {
            // if we have gotten far enough into the animation to apply force to the movement 
           if(normalizedTime >= currentAttack.ForceTime )
            {
              TryApplyForce();
            }

           if(stateMachine.InputReader.IsAttacking)
            {
              TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState( new PlayerTargetingState(stateMachine));
            } 
            else
            {
                stateMachine.SwitchState( new PlayerFreeLookState(stateMachine));
            }
        }

    }

   
    public override void Exit()
    {
        
    }


    private void TryComboAttack(float normalizedTime)
    {
        if(currentAttack.ComboStateIndex == -1) {return;} // if there is not a next attack for the combo lined up 
        if(normalizedTime < currentAttack.ComboAttackTime){return;} // if the animation hasn't finished yet to go on to next attack in combo 

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, currentAttack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if(!forceApplied)
        {
            stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * currentAttack.Force); // applying the players forward direction by force of attack
            forceApplied = true; // as we only want the force to be applied once and not every update of the state
        }
    }

}
