using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
   protected PlayerStateMachine stateMachine; // will hold reference to player and let us change the state 

   public PlayerBaseState(PlayerStateMachine stateMachine)
   {
      this.stateMachine = stateMachine; // assigns to the incoming statemachine
        
   }

   protected void Move(float deltaTime)
   {
      Move(Vector3.zero, deltaTime);
   }

   protected void Move(Vector3 Motion, float DeltaTime) // motion being the state specific movement its being called from 
   {
      // use of the controller handles collisions
      stateMachine.Controller.Move((Motion + stateMachine.ForceReciever.Movement) * DeltaTime);
   } 



   protected void FaceTarget()
   {
      if(stateMachine.Targeter.CurrentTarget != null)
      {
         Vector3 TargetPos = stateMachine.Targeter.CurrentTarget.transform.position;
         Vector3 PlayerPos = stateMachine.transform.position;

         Vector3 DirectionToTarget = TargetPos - PlayerPos;

         DirectionToTarget.y = 0f; // dont want the ypos of target to effect player

         stateMachine.transform.rotation = Quaternion.LookRotation(DirectionToTarget); // turns the vector into Quaterian for us 
      }
   }
}
