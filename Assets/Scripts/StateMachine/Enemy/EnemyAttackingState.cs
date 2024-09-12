using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackinngState : EnemyBaseState
{
    private readonly int AttackTreeHash = Animator.StringToHash("Attack");
    private const float CrossFadeDuration = 0.1f;

    public EnemyAttackinngState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
      
    }

    public override void Exit()
    {
    }


}
