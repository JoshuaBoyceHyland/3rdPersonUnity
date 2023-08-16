using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack 
{
   [field: SerializeField] public string AnimationName {get; private set;}
   private Vector3 impact;
    private Vector3 dampingVelocity;
   [field: SerializeField] public float TransitionDuration {get; private set;} // 

   [field: SerializeField] public int ComboStateIndex {get; private set;} = -1;// this works as the direction to the next attack

   [field: SerializeField] public float ComboAttackTime {get; private set;} // how far throught the attack you will be allowed to do the next attack

   [field: SerializeField] public float ForceTime{get; private set;} //how long into animation you want to apply the force

   [field: SerializeField] public float Force {get; private set;} // the power of the force when the player atttack with this animation
    
   
}
