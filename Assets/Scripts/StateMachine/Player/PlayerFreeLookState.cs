using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state handles player when they are running around not locked on to anything
public class PlayerFreeLookState : PlayerBaseState
{

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){} // required for the base class

    // read only means as soon as assigned can not be changed, used for things like below which will be assigned at run time. In which case const will not work 
    private readonly int FreelookHash = Animator.StringToHash("FreeLookSpeed"); // hash is an integer id, quicker than user integer or string
    private readonly int FreelookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    const float AnimatorDampTime = 0.1f;
    
    public override void Enter()
    {
    
        stateMachine.InputReader.JumpEvent+=OnJump; // subscribes the event to trigger the method if called
        stateMachine.InputReader.TargetingToggleEvent+=OnTargetToggle; 
        stateMachine.Animator.Play(FreelookBlendTreeHash); // goes to the free look animator blend tree upon entering the state
    }

    public override void Tick(float deltaTime)
    {

        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.StandardMovementSpeed, deltaTime); 
        
        // if there is no movement we idle
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreelookHash, 0, AnimatorDampTime, deltaTime); // specify the variable, the valure you want to set to, the damptime (smoothing value)
            return;
        }
        stateMachine.Animator.SetFloat(FreelookHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);

    }

    

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent-=OnJump;
        stateMachine.InputReader.TargetingToggleEvent-=OnTargetToggle;
       
    }



    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        
    }

    private void OnTargetToggle()
    {
      
        if(stateMachine.Targeter.SelectTarget())
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
    
    private Vector3 CalculateMovement()
    {

        Vector3 CamerasForward = stateMachine.MainCameraTransform.forward;
        Vector3 CamerasRight = stateMachine.MainCameraTransform.right;
        Vector3 NewCameraRelativeMovement = new Vector3();

        CamerasForward.y = 0;
        CamerasRight.y = 0; 

        CamerasForward.Normalize();
        CamerasRight.Normalize();

        NewCameraRelativeMovement = CamerasForward * stateMachine.InputReader.MovementValue.y + CamerasRight * stateMachine.InputReader.MovementValue.x;
        

        return NewCameraRelativeMovement;
    }

    private void FaceMovementDirection(Vector3 movement, float DeltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, // current rotation
            Quaternion.LookRotation(movement), // goal rotation point
            DeltaTime * stateMachine.RotationSmooth // makes sure the smooth is independent of framerate
        );
    }
}
