using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damageToPlayer = 25;
    [SerializeField] private float damageToBoss = 10;
    [SerializeField] private float despawnTime = 5;
    private float time;
    private void Update()
    {
        time = time + Time.deltaTime;
        if(time >= despawnTime)
        {
            Despawn();
            time = 0;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player got hit for" + damageToPlayer + "of damage");
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossHealth>().TakeDamage(damageToBoss);
        }
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
