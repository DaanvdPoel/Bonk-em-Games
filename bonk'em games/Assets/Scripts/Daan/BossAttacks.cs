using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 100;
    [SerializeField] private float attackCooldown;
    private float time;
     
    private void Update()
    {
        time = time + Time.deltaTime;
        if(time >= attackCooldown)
        {
            GameObject temp = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(transform.forward.x, 0, transform.forward.z) * 1.1f), Quaternion.identity);
            temp.transform.rotation = transform.rotation;
            temp.GetComponent<Rigidbody>().AddForce((new Vector3(transform.forward.x, 0, transform.forward.z)) * bulletSpeed);
            time = 0;
        }
    }
}
