using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int IdleBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}


 
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(IdleBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(IsInChaseRange())
        {
            Debug.Log("In Range");
            // change to chase state eventually
        }

        // seting our animator to be at idle state through our speed parameter in locomotion blend tree
        stateMachine.Animator.SetFloat(SpeedHash, 0, AnimatorDampTime, deltaTime); 
    }
    public override void Exit()
    {

    }

   


}
