using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");


    public override void Enter()
    {
        Debug.Log("entering targeting state");
        stateMachine.InputReader.TargetingToggleEvent+=OnTargetToggle;
        stateMachine.Animator.Play(TargetingBlendTreeHash); // goes to the targeting animator blend tree upon entering the state
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log(stateMachine.Targeter.CurrentTarget.name);

        // this will exit current state if they player leaves our radius of targeting, they go out of range 
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return; 
        }

        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetingToggleEvent-=OnTargetToggle;
        Debug.Log("exiting targeting state");
    }

    private void OnTargetToggle()
    {
        Debug.Log("targeting toggled");
        stateMachine.Targeter.Cancel(); // sets the currentTarget to null
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

  
}
