using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerHealth;

    public void TakeDamage(float amount)
    {
        playerHealth = playerHealth - amount;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (playerHealth <= 0)
            UIManager.instance.DeathScreen();
    }
}
