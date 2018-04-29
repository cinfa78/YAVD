using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    float intensity = 1f;
    bool isShaking;
    Transform restTransform;
    Vector3 restPosition;
    Quaternion restRotation;
    private void Start()
    {
        isShaking = false;
        restTransform = transform;
        restPosition = transform.localPosition;
        restRotation = transform.localRotation;
        print(restRotation.eulerAngles);
    }
    public void Shake()
    {
        if(!isShaking)
            StartCoroutine(ShakeCoroutine());
    }
    public IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        Vector3 newAngle;
        while (isShaking)
        {
            //transform.position = restPosition.position + Vector3.one * Random.Range(-intensity, intensity);
            newAngle = Vector3.one* Random.Range(-intensity, intensity) + restRotation.eulerAngles;
            print(newAngle);
            transform.localRotation = Quaternion.Euler(newAngle);
            yield return null;
        }
        isShaking = false;
    }

    public void StopShake()
    {
        isShaking = false;
        StopAllCoroutines();
        ResetTransform();
    }

    void ResetTransform()
    {
        transform.position = restPosition;
        transform.rotation = restRotation;
        transform.localScale = restTransform.localScale;
    }
}
