using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBossAnimTest : MonoBehaviour
{
    [SerializeField] private Animator m_animator;                                   // References the animation component.
    [SerializeField] private AnimationClip m_animClip;                              // References the clip to be played.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_animator.SetTrigger("Attack");
        }
    }
}
