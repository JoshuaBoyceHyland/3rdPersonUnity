using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private int health;
    [SerializeField] private int maxHealth = 100; 


    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if(health == 0){return;}
        // this will assign health to the bigger of the passed in arguments, which will prevent it from going below zero
        health = Mathf.Max( health - damage, 0); 

        Debug.Log(health);
    }
}
