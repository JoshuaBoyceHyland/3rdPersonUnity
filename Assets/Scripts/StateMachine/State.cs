using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(); 
    public abstract void Tick(float deltaTime);
    public abstract void Exit(); 

    
    protected float GetNormalisedTime(Animator animator)
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0); // get info of current animation stay on layer 1
        AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(0); 

        if(animator.IsInTransition(0) && nextStateInfo.IsTag("Attack")) // if we are transitioning to an attack animaton send that animation normalized time
        {
            return nextStateInfo.normalizedTime;
        }
        else if(!animator.IsInTransition(0) && currentStateInfo.IsTag("Attack")) // otherwise if we are not transitioning but still in an attack animation send the current normalized time
        {
            return currentStateInfo.normalizedTime;
        }


        return 0f;
    }
}

