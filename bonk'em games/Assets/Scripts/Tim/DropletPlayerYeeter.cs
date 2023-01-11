using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;

public class DropletPlayerYeeter : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed;             // How fast thye droplet rotates on y axis as its animation.

    [Space]
    [SerializeField] private AnimationCurve m_playerArc;        // How the player should arc. The x axis is distance on world z  position from player, the y axis is height.
    [SerializeField] private float m_launchSpeed;               // How fast the player reaches the destination.

    private bool m_launchPlayer;                                // True when player enters collider;
    private Vector3 m_playerStartPos;                           // Notes the start position to launch the player from
    private GameObject m_player;                                // Reference to the object.
    private float m_launchTime = 0;                             // Time since start launch.

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, (Mathf.Repeat(Time.time, 360 / m_rotationSpeed) * m_rotationSpeed), 0));

        if (m_launchPlayer) { LaunchPlayer(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject _otherObject = other.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            m_player = _otherObject;
            m_playerStartPos = _otherObject.transform.position;
            m_launchPlayer = true;
        }      
    }

    private void LaunchPlayer()
    {
        var _lastKeyFrame = m_playerArc.keys[m_playerArc.length - 1];
        if (m_launchTime >= _lastKeyFrame.time)
        {
            m_launchPlayer = false;
            m_launchTime = 0;
            return;
        }

        Vector3 _newPos = new Vector3(m_playerStartPos.x, m_playerStartPos.y + m_playerArc.Evaluate(m_launchTime), m_playerStartPos.z + m_launchTime);
        m_player.transform.position = _newPos;

        float _newLaunchTime = m_launchTime + (Time.deltaTime * m_launchSpeed);
        m_launchTime = _newLaunchTime;
    }
}
