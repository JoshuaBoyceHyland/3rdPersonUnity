using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  EnemyBaseState : State
{

   protected EnemyStateMachine stateMachine;

   public EnemyBaseState(EnemyStateMachine stateMachine)
   {
      this.stateMachine = stateMachine; // assigns to the incoming statemachine   
   }

   
}
