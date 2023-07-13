using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state handles player when they are running around not locked on to anything
public class PlayerFreeLookState : PlayerBaseState
{

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){} // required for the base class
    private float stateDuration = 0f;
    public override void Enter()
    {
        Debug.Log("entering");
        stateMachine.InputReader.JumpEvent+=OnJump; // subscribes the event to trigger the method if called
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        // use of the controller handles collisions
        stateMachine.Controller.Move(movement * stateMachine.StandardSpeed * deltaTime);

        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime); // specify the variable, the valure you want to set to, the damptime (smoothing value)
            return;
        }

        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime); 
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
     
    }
    
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent-=OnJump;
        Debug.Log("exiting");
    }



    private void OnJump()
    {
        Debug.Log("Jumped");
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        
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


}
