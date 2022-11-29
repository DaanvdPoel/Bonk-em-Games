using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoss : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float rotationSpeed = 15;
    Quaternion rotarionTarget;

    private void Update()
    {
        RotateToPlayer();
    }

    private void RotateToPlayer()
    {
        rotarionTarget = Quaternion.LookRotation(player.transform.position - transform.position);

        rotarionTarget.x = Mathf.Clamp(rotarionTarget.eulerAngles.x, 0, 0);
        rotarionTarget.z = Mathf.Clamp(rotarionTarget.eulerAngles.z, 0, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotarionTarget, rotationSpeed * Time.deltaTime);
    }


}
