using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_particleToPlay;

    private void OnTriggerEnter(Collider other)
    {
        var _particlePlayPosition = transform.position;

        m_particleToPlay.transform.position = _particlePlayPosition;

        m_particleToPlay.gameObject.SetActive(true);
        m_particleToPlay.Play();
    }
}
