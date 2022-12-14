using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float bossMaxHealth = 100;
    [SerializeField] private HealthBar healthBar;
    private float health;

    void Start()
    {
        health = bossMaxHealth;
        healthBar.SetMaxHealth(bossMaxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") == true)
        {
            if (other.gameObject.GetComponent<RoundBullet>().time >= 1)
            {
                TakeDamage(other.gameObject.GetComponent<RoundBullet>().damageToBoss);
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        healthBar.SetHealth(health);
        health = health - amount;
        Debug.Log("boss got hit for " + amount + " of damage");
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            Debug.Log("Boss is dead");
            UIManager.instance.WinScreen();
        }
    }
}
