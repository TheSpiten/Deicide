using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;

    // Determines by how much we decrement shakeDuration. 1.0f means shakeDuration is in seconds.
    private float altDuration = 1.0f;
    Vector3 startPos;

    private void Awake()
    {
        if (transform == null)
            transform = GetComponent(typeof(Transform)) as Transform;
    }

    private void OnEnable()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = startPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * altDuration;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = startPos;
        }
    }

    public void Shake()
    {
        shakeDuration = 0.2f;
    }
}
