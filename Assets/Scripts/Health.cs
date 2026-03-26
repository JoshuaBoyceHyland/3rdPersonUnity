using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    private int health;

    private bool isInvunerable;
    [SerializeField] private int maxHealth = 100; 

    public event Action OnTakeDamage;
    public event Action OnDeath;
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void setInvurnable( bool t_isInvurnable)
    {
        this.isInvunerable = t_isInvurnable;
    }

    public void DealDamage(int damage)
    {
        if( isInvunerable) {return;}
        if(health == 0){return;}
        // this will assign health to the bigger of the passed in arguments, which will prevent it from going below zero
        health = Mathf.Max( health - damage, 0); 
        OnTakeDamage?.Invoke();

        if( health <= 0 )
        {
            OnDeath?.Invoke();   
        }

        Debug.Log(health);
    }
}
