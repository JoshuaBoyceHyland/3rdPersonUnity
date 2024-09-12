using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{

    private readonly int LocomtionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomtionBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {


        if( !IsInChaseRange() )
        {
            Debug.Log("Out of range Range");
            stateMachine.SwitchState( new EnemyIdleState(stateMachine));
            return;
        }
        else if( IsInAttackRange() )
        {
            stateMachine.SwitchState( new EnemyAttackinngState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        // seting our animator to be at idle state through our speed parameter in locomotion blend tree
        stateMachine.Animator.SetFloat(SpeedHash, 1, AnimatorDampTime, deltaTime); 
    }
    public override void Exit()
    {
        // no longer trying to move towards player
        stateMachine.NavAgent.ResetPath(); 
        stateMachine.NavAgent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float DeltaTime)
    {
        stateMachine.NavAgent.destination = stateMachine.Player.transform.position; 
        Move(stateMachine.NavAgent.desiredVelocity.normalized * stateMachine.MovementSpeed, DeltaTime); // here we are using direction from the nav agent to move the enemy in direction of the player
        stateMachine.NavAgent.velocity = stateMachine.Controller.velocity; // updating the nav agent as we dont have it set up to do this automatically
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSquared = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude; 
         
        return playerDistanceSquared <= stateMachine.AttackRange * stateMachine.AttackRange;
    }


}
