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

   protected void Move(float deltaTime)
   {
      Move(Vector3.zero, deltaTime);
   }

   protected void Move(Vector3 Motion, float DeltaTime) // motion being the state specific movement its being called from 
   {
      // use of the controller handles collisions
      stateMachine.Controller.Move((Motion + stateMachine.ForceReciever.Movement) * DeltaTime);
   } 
   
   protected bool IsInChaseRange()
   {
      // previous way I did which was less efficient as it used magnitude which is less efficeint oto do 
      //Vector3 distanceToPlayer = stateMachine.Player.transform.position - stateMachine.transform.position; 
      //return distanceToPlayer.magnitude <= stateMachine.PlayerDetectionRange; 
      
      float distanceToPlayerSqrt = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude; 

      return distanceToPlayerSqrt <= stateMachine.PlayerDetectionRange * stateMachine.PlayerDetectionRange; // if distance to player is lesser than the range it is close enough to begin chasing
      
      
   }
   
}
