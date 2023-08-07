using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
   public event Action<Target> OnDestroyed; // <> means passes an argument of this type when invoked



   private void OnDestroy()
   {
     OnDestroyed?.Invoke(this); // also checking is not null
   }
}
