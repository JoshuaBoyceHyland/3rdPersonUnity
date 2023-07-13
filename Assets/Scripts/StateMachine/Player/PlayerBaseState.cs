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
}
