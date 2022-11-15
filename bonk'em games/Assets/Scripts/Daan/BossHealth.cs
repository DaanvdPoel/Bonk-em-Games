using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float bossMaxHealth = 100;
    private float health;

    void Start()
    {
        health = bossMaxHealth;    
    }

    public void TakeDamage(float amount)
    {
        health = health - amount;
        Debug.Log("boss got hit for " + amount + " of damage");
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            Debug.Log("Boss is dead");
        }
    }
}
