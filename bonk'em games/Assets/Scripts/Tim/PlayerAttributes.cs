using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum SwingStates
{
    nothing,
    normalSwing,
    heavySwing
}

[System.Serializable]
public struct SwingClipState
{
    public SwingStates swingState;                      // name of the associated state.
    public AnimationClip clip;                          // Animation to play.
    public Vector2 m_startAndEndRockHitWindow;          // When the rock can hit the hammer in the animation, Value is clamped between 0 and clip length.
}

[RequireComponent(typeof(Animator))]
public class PlayerAttributes : MonoBehaviour
{
    public SwingClipState[] allSwingStates;                                  // Store the hitwindow and clip associated with it.

    [HideInInspector] public Animator animator;                                 // Reference to the hammers animation component.
    [HideInInspector] public AnimationClip currentClip;                         // Reference to the current clip, only update when change occurs.
    
    [HideInInspector] public SwingClipState currentSwingState;

    private void Start()
    {
        // Set the currentclipstate to normalswing due to only having 1 atk for now.
        foreach (var _clipState in allSwingStates)
        {
            if (_clipState.swingState == SwingStates.normalSwing)
            {
                currentSwingState = _clipState;
            }
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DoSwingAttribute();
    }

    private void DoSwingAttribute()
    {
        // for now we have only the normal attribute

        switch (currentSwingState.swingState)
        {
            case SwingStates.nothing:
                if (Input.GetMouseButtonUp(0)) { SwingHammer(SwingStates.normalSwing); }
                break;

            case SwingStates.normalSwing:
                break;

            case SwingStates.heavySwing:
                break;

            default:
                Debug.LogError("Switch statement defaulted.");
                break;
        }
    }

    private void SwingHammer(SwingStates stateToGoTo)
    {
        animator.Play("Hammer|HammerAction");

        foreach (var _state in allSwingStates)
        {
            if (_state.swingState == stateToGoTo)
            {
                currentSwingState = _state;
            }
        }
    }
}
