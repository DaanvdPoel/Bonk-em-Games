using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxPlayerHealth = 100;
    [SerializeField] private HealthBar healthBar;
    private float playerHealth;
    private void Start()
    {
        playerHealth = maxPlayerHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
    }
    public void TakeDamage(float amount)
    {
        playerHealth = playerHealth - amount;
        healthBar.SetHealth(playerHealth);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (playerHealth <= 0)
            UIManager.instance.DeathScreen();
    }
}
