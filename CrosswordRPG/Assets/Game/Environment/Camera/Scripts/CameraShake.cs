using System.Collections;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    public void Invoke(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 startPosition = transform.localPosition;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3 (x, y, startPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        } 
        transform.localPosition = startPosition;
    }

}