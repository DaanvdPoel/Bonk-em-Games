using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingV2 : MonoBehaviour
{
    [SerializeField] private SwingManager m_swingManager;

    [SerializeField] private Animator m_animator;                                   // References the animation component.
    [SerializeField] private AnimationClip m_animClip;                              // References the clip to be played.
    [SerializeField] private BoxCollider m_hammerCollider;                          // Reference tot he attached collider.

    [Space]
    [SerializeField] private Transform m_explosionForceOrigin;                      // Place where the explosion force applied to the bullet orifinates from.
    [SerializeField] private float m_explosionForceStrength;                        // How strong the applied force to the bullet should be.
    [SerializeField] private float m_explosionForceRadius;                          // How big the explosions radius should be.
    [SerializeField] private float m_extraUpForce;

    [Space]
    [SerializeField] private Vector2 m_startAndEndRockHitWindow;                    // When the rock can hit the hammer in the animation, Value is clamped between 0 and clip length.
    private bool m_doOnce = false;

    private void Start()
    {
        m_hammerCollider.enabled = false;
        m_animator.speed = 3;
    }

    private void Update()
    {
        SwingWindow();
    }

    private void SwingWindow()
    {
        if (Input.GetMouseButtonUp(0) && SwingManager.canSwing && !m_doOnce)
        {
            m_doOnce = true;

            m_animator.Play(m_animClip.name);
            Debug.Log(m_animClip.name);

            Debug.Log("hammer is swinging");

            // Correct the player inputted m_startAndEndRockHitWindow value's if it's outside the clip lenght bounds.
            var _vectorX = Mathf.Clamp(m_startAndEndRockHitWindow.x, 0, m_animClip.length);
            var _vectorY = Mathf.Clamp(m_startAndEndRockHitWindow.y, 0, m_animClip.length);

            m_startAndEndRockHitWindow.x = _vectorX;
            m_startAndEndRockHitWindow.y = _vectorY;

            if (m_startAndEndRockHitWindow.x > m_animClip.length) { Debug.LogWarning("Becarefull, m_startAndEndRockHitWindow start variable higher than clip length."); }

            // Setup and iterate a timer
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        m_hammerCollider.enabled = false;

        yield return new WaitForSeconds(m_startAndEndRockHitWindow.x / m_animator.speed);
        m_hammerCollider.enabled = true;

        yield return new WaitForSeconds(m_startAndEndRockHitWindow.y / m_animator.speed);
        m_hammerCollider.enabled = false;

        yield return new WaitForSeconds((m_animClip.length / m_animator.speed) - (m_startAndEndRockHitWindow.y / m_animator.speed));

        Debug.Log((m_animClip.length * m_animator.speed) - m_startAndEndRockHitWindow.y);
        SwingManager.canSwing = true;
        m_doOnce = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("KABLOOEI");
            var _bulletRB = collision.gameObject.GetComponent<Rigidbody>();
            _bulletRB.AddExplosionForce(m_explosionForceStrength, m_explosionForceOrigin.position, m_explosionForceRadius);
            _bulletRB.AddForce(Vector3.up * m_extraUpForce, ForceMode.Impulse);
        }
    }
}
