using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetinForwardTreeHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightTreeHash = Animator.StringToHash("TargetingRight");

    public override void Enter()
    {
       
        stateMachine.InputReader.TargetingToggleEvent+=OnTargetToggle;
        stateMachine.Animator.Play(TargetingBlendTreeHash); // goes to the targeting animator blend tree upon entering the state
    }
    public override void Tick(float deltaTime)
    {
        // this will exit current state if they player leaves our radius of targeting, they go out of range 
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return; 
        }

        Vector3 movement = CalculateMovement(); 
        Move(movement * stateMachine.TagretingMovementSpeed, deltaTime); 

        UpdateAnimator(deltaTime);

        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetingToggleEvent-=OnTargetToggle;
    
    }

    private void OnTargetToggle()
    {
       
        stateMachine.Targeter.Cancel(); // sets the currentTarget to null
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    
    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        // multiplies inputed movement by the player position, considering its rotation also
        movement +=  stateMachine.transform.right * stateMachine.InputReader.MovementValue.x; 
        movement +=  stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement; 
    }

    private void UpdateAnimator(float deltaTime)
    {
        Vector3 CurrentMovement = stateMachine.InputReader.MovementValue; 


        if(CurrentMovement.y == 0)// will set to idle
        {
            stateMachine.Animator.SetFloat(TargetinForwardTreeHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float roundedValue = CurrentMovement.y > 0 ? 1f : -1f; // will check if currentMovement is greater than 0, if so will make it 1 otherwise it will be -1
            stateMachine.Animator.SetFloat(TargetinForwardTreeHash, roundedValue, 0.1f, deltaTime); // need to round so it can be then used to select correct animation 
        }

        if(CurrentMovement.x == 0) 
        {
            stateMachine.Animator.SetFloat(TargetingRightTreeHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float roundedValue = CurrentMovement.x > 0 ? 1f : -1f; 
            stateMachine.Animator.SetFloat(TargetingRightTreeHash, roundedValue, 0.1f, deltaTime);
        }

    }
}
