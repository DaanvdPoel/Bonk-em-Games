using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Unityeditor accesible variables:
    [Header("Setttings")]
    [SerializeField] private Transform m_orientation;
    [SerializeField] private GameObject m_jumpForceOrigin;

    [Space]
    [SerializeField] private bool m_useSlopeAngle = true;                       // Use angle based ground detection.

    [Range(0, 90)]
    [Space, SerializeField] private float m_maxSlopeAngle;                      // Prevents jumping from ground angled higher.

    [SerializeField] private LayerMask m_groundLayer;                           // The layer jumpable ground shares.
    [SerializeField] private float m_raycastLength;                             // Detects if the player is grounded.

    [Space]
    [SerializeField] private bool m_enableRunning = true;                       // Enable the ability to run.
    [SerializeField] private bool m_enableAirDash = false;                      // Enable the ability to dash onnce while airborn.

    [Space]
    [SerializeField] private float m_movementSpeed;                             // How fast the player move in M/S.
    [SerializeField] private float m_runSpeedMultiplier;                          // How fast the player moves while running in M/S.
    [SerializeField] private float m_jumpForce;                                 // How much force is applied to jump.
    [SerializeField] private float m_airDashForce;                              // Force applied when airdashing.


    [Space]
    [SerializeField] private float m_mouseSensitivity;                          // Sensitivity of mouse.


    // Unityeditor unaccesible variables:
    private Rigidbody m_rigidBody;                                              // Reference to the attached rigidbody.

    private bool m_isGrounded = false;                                           // True on collision enter.
    private bool m_canJump = true;                                              // True when player can jump again.
    private bool m_hasDashed = true;                                            // Restrains from dashing multiple times.

    private float m_desiredX;                                                   // Keeps track of the desired x rotation.
    private float m_xRotation;                                                  // Keeps track of the actual x rotation.    
    private float m_currentMoveSpeed;                                            // Keeps track of the current run speed.

    private float m_xSpeed;                                                     // keeps track of the desired speed on X Axis.
    private float m_ySpeed;                                                     // keeps track of the desired speed on Y Axis.

    private void Start()
    {
        try { m_rigidBody = GetComponent<Rigidbody>(); } catch { Debug.Log($"Could not find: \"RigidBody\", On object: {gameObject.name}."); }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        DetectGround();

        Movement();
        Jump();    
    }
    private void FixedUpdate()
    {
        PhysicsMovement();
        AirDash();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }


    private void AirDash()
    {
        if (!m_isGrounded && Input.GetKeyDown(KeyCode.Space) && !m_hasDashed)
        {
            //Vector3 _movePos = (m_orientation.transform.right * m_xSpeed + m_orientation.transform.forward * m_airDashForce);
            //Vector3 _newMovePos = new Vector3(_movePos.x, m_rigidBody.velocity.y, _movePos.z);

            //Vector3 _newvel = new Vector3(m_rigidBody.velocity.x, 0, 0).normalized;
            
            m_rigidBody.AddForce(m_orientation.transform.forward * m_airDashForce, ForceMode.Impulse);
            m_hasDashed = true;
        }
    }


    /// <summary>
    /// Responsible for applying movement force and enforcing its conditions.
    /// </summary>
    private void Movement()
    {
        //Vector3 _newVelocity = new Vector3();
        //if (m_isGrounded || (!m_isGrounded && Input.GetAxisRaw("Horizontal") != 0))
        //{
        //    _newVelocity = new Vector3(Input.GetAxisRaw("Horizontal") * m_movementSpeed, m_rigidBody.velocity.y, 0);
        //    if (Input.GetAxisRaw("Horizontal") == m_previousMovementKey) { return; }
        //    m_previousMovementKey = (int)Input.GetAxisRaw("Horizontal");
        //    m_rigidBody.velocity = _newVelocity;
        //}

        m_xSpeed = Input.GetAxisRaw("Horizontal") * m_movementSpeed * m_currentMoveSpeed;
        m_ySpeed = Input.GetAxisRaw("Vertical") * m_movementSpeed;

        if (m_enableRunning)
        {
            if (Input.GetKey(KeyCode.LeftShift) && m_isGrounded)
            {
                m_currentMoveSpeed = m_runSpeedMultiplier;
            }
            else { m_currentMoveSpeed = 1;}
        }
    }

    private void PhysicsMovement()
    {
        if (m_hasDashed) { return; }
        Vector3 _movePos = (m_orientation.transform.right * m_xSpeed + m_orientation.transform.forward * m_ySpeed * m_currentMoveSpeed);
        Vector3 _newMovePos = new Vector3(_movePos.x, m_rigidBody.velocity.y, _movePos.z);

        m_rigidBody.velocity = _newMovePos;

    }


    /// <summary>
    /// Responsible for applying jumping force and enforcing its conditions.
    /// </summary>
    private void Jump()
    {
        if (!m_isGrounded) { m_canJump = false; }
        else { m_canJump = true; }
        if (Input.GetKeyDown(KeyCode.Space) && m_canJump && m_isGrounded)
        {
            //m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_rigidBody.AddExplosionForce(m_jumpForce, m_jumpForceOrigin.transform.position, 100);
            m_isGrounded = false;
            m_canJump = false;
        }
    }

    /// <summary>
    /// Logic for detection when on ground.
    /// </summary>
    /// <returns>True if on ground and slope < maxSlopeAngle. </returns>
    private void DetectGround()
    {
        switch (m_useSlopeAngle)
        {
            case true:
                RaycastHit _ray;
                if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out _ray, m_raycastLength))
                {
                    Vector3 _normalToEuler = Quaternion.FromToRotation(Vector3.up, _ray.normal).eulerAngles;
                    if (_normalToEuler.z < m_maxSlopeAngle)
                    {
                        m_isGrounded = true;
                        m_hasDashed = false;
                        return;
                    }
                }
                break;

            case false:
                if (Physics.Raycast(gameObject.transform.position, -Vector3.up, m_raycastLength, m_groundLayer))
                {
                    m_isGrounded = true;
                    m_hasDashed = false;
                    return;
                }
                break;
        }
        m_isGrounded = false;
    }

    /// <summary>
    /// Handles the camera rotation depending on mouse input.
    /// </summary>
    private void CameraRotation()
    {
        //Camera Look/Rotion Logic.
        float _mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.fixedDeltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.fixedDeltaTime;

        if (Camera.main.gameObject.activeInHierarchy)
        {
            Vector3 _rot = Camera.main.transform.localRotation.eulerAngles;
            m_desiredX = _rot.y + _mouseX;

            m_xRotation -= _mouseY;
            m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

            Camera.main.transform.localRotation = Quaternion.Euler(m_xRotation, m_desiredX, 0);
            m_orientation.transform.localRotation = Quaternion.Euler(0, m_desiredX, 0);
        }
    }
}
