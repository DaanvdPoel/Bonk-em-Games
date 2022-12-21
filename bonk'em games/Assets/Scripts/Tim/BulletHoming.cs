using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : MonoBehaviour
{
    [SerializeField] private float m_steerForce;                // How hard to steer to the boss.
    [SerializeField] private float m_homingCancelDistance;
    [SerializeField] private float m_forwardForce;
    [SerializeField] private float m_speedLimit;

    //[SerializeField] private LayerMask m_homingFieldLayer;

    private Rigidbody m_rb;
    //private Collider m_currentCol;
    private GameObject m_homingTarget;
    private bool m_hasHomed;

    [HideInInspector] public bool m_hasBounced;                 // Set to true when player hits it with the hammer, Enables homing.

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_homingTarget = GameObject.FindGameObjectsWithTag("Boss")[0];
    }

    private void OnDisable()
    {
        m_hasBounced = false;
    }

    private void FixedUpdate()
    {
        Homing();
        
    }

    private void LateUpdate()
    {
        SpeedLimiter();
    }

    private void SpeedLimiter()
    {
        if (m_rb.velocity.magnitude > m_speedLimit)
        {
            m_rb.velocity = m_rb.velocity.normalized * m_speedLimit;
        }
    }

    private void Homing()
    {
        // TODO, send a raycast and calculate the distance to the boss on each point of the ray if its in a certain distance of the boss activate the homing.

        transform.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);

        if (!m_hasBounced) { return; }

        var _distance = (m_homingTarget.transform.position - transform.position).magnitude;

        // Cancel's the homing when in distance from boss
        if (_distance < m_homingCancelDistance) { m_hasHomed = false; }

        if (!m_hasHomed)
        {
            var _rotationTarget = Quaternion.LookRotation(m_homingTarget.gameObject.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationTarget, m_steerForce * Time.deltaTime);

            m_rb.AddForce(m_rb.gameObject.transform.forward * m_forwardForce, ForceMode.VelocityChange);
        }



        //transform.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);

        //if (!m_hasBounced) { return; }

        //var _distance = (m_homingTarget.transform.position - transform.position).magnitude;

        //// Cancel's the homing when in distance from boss
        //if (_distance < m_homingCancelDistance) { m_hasHomed = false; }

        //if (!m_hasHomed)
        //{
        //    var _rotationTarget = Quaternion.LookRotation(m_homingTarget.gameObject.transform.position - transform.position);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationTarget, m_steerForce * Time.deltaTime);

        //    m_rb.AddForce(m_rb.gameObject.transform.forward * m_forwardForce, ForceMode.VelocityChange);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * 5);

        if (m_homingTarget != null)
        {
            Gizmos.color = Color.white;
            var _vector = (m_homingTarget.gameObject.transform.position - transform.position).normalized;
            Gizmos.DrawRay(transform.position, _vector * 5);
        }
    }
}
