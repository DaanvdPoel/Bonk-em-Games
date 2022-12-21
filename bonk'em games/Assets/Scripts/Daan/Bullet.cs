using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageToPlayer = 25;
    public float damageToBoss = 10;
    [SerializeField] private float despawnTime = 5;
    [SerializeField] private int maxBounceCount = 3;
    [SerializeField] private float bounciness = 0.8f;
    private int bounceCount;
    public float time;

    private void Awake()
    {
        gameObject.GetComponent<BoxCollider>().material.bounciness = bounciness;
    }

    private void Update()
    {
        time = time + Time.deltaTime;
        if (time >= despawnTime || bounceCount == maxBounceCount)
        {
            Despawn();
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player got hit for " + damageToPlayer + " of damage");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
        }

        if (!collision.gameObject.CompareTag("Boss") && !collision.gameObject.CompareTag("Player") && time >= 0.5)
        {
            bounceCount = bounceCount + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") && time >= 0.5)
        {
            other.gameObject.GetComponent<BossHealth>().TakeDamage(damageToBoss);
            Destroy(gameObject);
        }

    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
