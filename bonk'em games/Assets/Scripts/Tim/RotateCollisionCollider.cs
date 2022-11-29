using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCollisionCollider : MonoBehaviour
{
    [SerializeField] private Transform m_zForwardReference;         // The z forward world rotation to copy.

    void Update()
    {
        gameObject.transform.rotation = m_zForwardReference.rotation;
    }
}
