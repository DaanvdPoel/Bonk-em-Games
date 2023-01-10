using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAttack : MonoBehaviour
{
    public GameObject bullet;
    public int bulletcount;
    public int bulletOnSpawn;
    public int bigBulletDamage;
    private bool groundHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            SpawnBullets();
            groundHit = true;
        }
        else if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bigBulletDamage);
        }

    }

    private void SpawnBullets()
    {
        for (int i = 0; i < bulletcount; i++)
        {
            if (groundHit == false)
            {
                Debug.Log("bullet spawned");
                GameObject temp = Instantiate(bullet, new Vector3(transform.position.x + Random.RandomRange(-4, 4), transform.position.y + Random.RandomRange(4, 8), transform.position.z + Random.RandomRange(-4, 4)), Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.RandomRange(-1, 1), Random.RandomRange(-1, 1), Random.RandomRange(-1, 1)) * bulletOnSpawn);
            }
        }
    }
}
