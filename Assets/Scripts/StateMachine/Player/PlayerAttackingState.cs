using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack currentAttack;

    // passes in the ID of the desired attack animation to be played
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, 0.1f); // plays the animation using the name set in the inspector and a fade time
    }

    public override void Tick(float deltaTime)
    {
     
    }

    public override void Exit()
    {
        
    }

}
