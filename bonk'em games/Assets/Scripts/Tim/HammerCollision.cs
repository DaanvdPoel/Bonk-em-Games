using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(BoxCollider))]
public class HammerCollision : MonoBehaviour
{
    [SerializeField] private PlayerAttributes m_playerAttributes;                   // Reference to the script.
    [SerializeField] private BoxCollider m_hammerCollider;                          // Reference tot he attached collider.

    [Space]
    [SerializeField] private Transform m_explosionForceOrigin;                      // Place where the explosion force applied to the bullet orifinates from.
    [SerializeField] private float m_explosionForceStrength;                        // How strong the applied force to the bullet should be.
    [SerializeField] private float m_explosionForceRadius;                          // How big the explosions radius should be.

    [Space]
    [SerializeField] private List<Vector2> m_startAndEndRockHitWindow;              // When the rock can hit the hammer in the animation, Value is clamped between 0 and clip length.

    private float m_currentSwingTimer = 0;                                          // Current time since start swing.
    private bool m_notTwice;                                                        // temp

    private void Start()
    {
        m_hammerCollider.enabled = false;
    }

    private void Update()
    {
        SwingWindow();
    }

    /// <summary>
    /// Makes the hitbox appear in the swings animation to add force to ricocheting bullet and makes it ricochet from hammer.
    /// </summary>
    private void SwingWindow()
    {
        // for now we only have a normalswing, this will be expandable.
        if (m_playerAttributes.currentSwingState.swingState != SwingStates.nothing)
        {
            switch (m_playerAttributes.currentSwingState.swingState)
            {
                case SwingStates.normalSwing:

                    if (true)
                    {
                        //StartCoroutine(Timer());
                    }

                    break;

                case SwingStates.heavySwing:
                    break;

                default:
                    Debug.LogError("Switch statement defaulted.");
                    break;
            }
        }
        else
        {
            //if ()
        }

        //if (true)
        //{
        //    Debug.Log("hammer is swinging");

        //    // Correct the player inputted m_startAndEndRockHitWindow value's if it's outside the clip lenght bounds.
        //    //m_startAndEndRockHitWindow.x = Mathf.Clamp(m_startAndEndRockHitWindow.x, 0, m_playerAttributes.previousAnim.clip.length);
        //    //m_startAndEndRockHitWindow.y = Mathf.Clamp(m_startAndEndRockHitWindow.y, 0, m_playerAttributes.previousAnim.clip.length);

        //    // Setup and iterate a timer
        //    m_currentSwingTimer += Time.deltaTime;
        //    CanAndSetHaveCollider();
        //}
        //else
        //{
        //    m_currentSwingTimer = 0;
        //    m_hammerCollider.enabled = false;
        //}
    }

   

    private bool CanAndSetHaveCollider()
    {
        m_hammerCollider.enabled = false;
        //if (m_currentSwingTimer > m_startAndEndRockHitWindow.x && m_currentSwingTimer < m_startAndEndRockHitWindow.y)
        //{
            //m_hammerCollider.enabled = true;
        //}

        return m_hammerCollider.enabled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("KABLOOEI");
            var _bulletRB = collision.gameObject.GetComponent<Rigidbody>();
            _bulletRB.AddExplosionForce(m_explosionForceStrength, m_explosionForceOrigin.position, m_explosionForceRadius);
        }
    }
}
