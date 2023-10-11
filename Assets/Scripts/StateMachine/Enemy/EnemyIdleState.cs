using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int LocomtionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}


 
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomtionBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(IsInChaseRange())
        {
            //Debug.Log("In Range");
            //stateMachine.SwitchState( new EnemyChasingState(stateMachine));
        }

        // seting our animator to be at idle state through our speed parameter in locomotion blend tree
        stateMachine.Animator.SetFloat(SpeedHash, 0, AnimatorDampTime, deltaTime); 
    }
    public override void Exit()
    {

    }

   


}
