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


   protected void Move(Vector3 Motion, float DeltaTime) // motion being the state specific movement its being called from 
   {
      // use of the controller handles collisions
      stateMachine.Controller.Move((Motion + stateMachine.ForceReciever.Movement) * DeltaTime);
   } 
}
