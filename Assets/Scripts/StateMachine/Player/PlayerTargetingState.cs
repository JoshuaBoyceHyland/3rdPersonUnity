using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}
    public override void Enter()
    {
        Debug.Log("entering targeting state");
        stateMachine.InputReader.TargetingToggleEvent+=OnTargetToggle;
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetingToggleEvent-=OnTargetToggle;
        Debug.Log("exiting targeting state");
    }

    private void OnTargetToggle()
    {
        Debug.Log("targeting toggled");
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
