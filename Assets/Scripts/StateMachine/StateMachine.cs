using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
   private State m_currentState;

    
    public void SwitchState(State newState)
    {
      m_currentState?.Exit(); // exits if current state is not null
      m_currentState = newState;
      m_currentState?.Enter(); // enters if the new state is not null

    }
    private void Update()
    {
      // if current state is not null it will tick
      // equivelent to if(m_currentState != null)
      m_currentState?.Tick(Time.deltaTime);
    }

  
}
