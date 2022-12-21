using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private CamRotationNoShake m_script;

    public void Play(float duration, float howHard)
    {
        StartCoroutine(CameraShake(duration, howHard));
    }

    private IEnumerator CameraShake(float duration, float howHard)
    {
        Vector3 _startPosition = transform.localPosition;

        float _timeSinceStart = 0f;

        while (_timeSinceStart < duration)
        {
            float _x = Random.Range(-1f, 1f) * howHard;
            float _y = Random.Range(-1f, 1f) * howHard;

            transform.localPosition = new Vector3(_x, _y, _startPosition.z);

            yield return null;
        }

        transform.localPosition = _startPosition;
    }
}
