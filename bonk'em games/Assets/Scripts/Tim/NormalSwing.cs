using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSwing : MonoBehaviour
{
    [SerializeField] private SwingManager m_swingManager;
    [SerializeField] private ParticleSystem m_hitParticle;                          // Reference to a particle played on hit.

    [SerializeField] private Animator m_animator;                                   // References the animation component.
    [SerializeField] private AnimationClip m_animClip;                              // References the clip to be played.
    [SerializeField] private BoxCollider m_hammerCollider;                          // Reference tot he attached collider.

    [Space]
    [SerializeField] private Transform m_explosionForceOrigin;                      // Place where the explosion force applied to the bullet orifinates from.
    [SerializeField] private float m_reflectForceStrength;                          // How strong the applied force to the bullet should be.
    //[SerializeField] private float m_explosionForceRadius;                          // How big the explosions radius should be.
    [SerializeField] private float m_extraUpForce;

    [Space]
    [SerializeField] private Vector2 m_startAndEndRockHitWindow;                    // When the rock can hit the hammer in the animation, Value is clamped between 0 and clip length.


    [Space]
    [SerializeField] private float m_impactShakeMagnitute;
    [SerializeField] private float m_impactShakeDuration;

    private bool m_doOnce = false;
    private CamShake m_camShakeScript;                                              // reerence to this script.

    private void Start()
    {
        m_hammerCollider.enabled = false;
        m_animator.speed = 1f;
        m_camShakeScript = FindObjectOfType<CamShake>();

        m_hitParticle.scalingMode = ParticleSystemScalingMode.Shape;
    }

    private void Update()
    {
        SwingWindow();

        if (Input.GetKeyDown(KeyCode.P))
        {
            //m_hitParticle.gameObject.SetActive(false);
            m_hitParticle.gameObject.SetActive(true);
        }
    }

    private void SwingWindow()
    {
        if (Input.GetMouseButtonUp(0) && SwingManager.canSwing && !m_doOnce)
        {
            m_doOnce = true;
            SwingManager.canSwing = false;

            m_animator.SetBool("DoSwing", true);
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
        else
        {
            m_doOnce = false;
        }
    }

    private IEnumerator Timer()
    {
        m_hammerCollider.enabled = false;

        yield return new WaitForEndOfFrame();
        m_animator.SetBool("DoSwing", false);

        yield return new WaitForSeconds(m_startAndEndRockHitWindow.x / m_animator.speed);
        m_hammerCollider.enabled = true;

        yield return new WaitForSeconds(m_startAndEndRockHitWindow.y / m_animator.speed);
        m_hammerCollider.enabled = false;

        yield return new WaitForSeconds((m_animClip.length / m_animator.speed) - (m_startAndEndRockHitWindow.y / m_animator.speed));

        Debug.Log((m_animClip.length * m_animator.speed) - m_startAndEndRockHitWindow.y);
        SwingManager.canSwing = true;
        m_doOnce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
           // m_hitParticle.gameObject.SetActive(false);
            //m_hitParticle.gameObject.SetActive(true);

            Debug.Log("KABLOOEI");

            var _bulletRB = other.gameObject.GetComponent<Rigidbody>();

            _bulletRB.AddForce(Camera.main.transform.forward * m_reflectForceStrength, ForceMode.Impulse);
        }
    }


}
