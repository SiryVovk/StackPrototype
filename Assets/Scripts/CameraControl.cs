using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.2f;

    private Coroutine moveRoutine;

    public void MoveUp(float amount)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveUpRoutine(amount));
    }

    private IEnumerator MoveUpRoutine(float amount)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * amount;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        moveRoutine = null;
    }
}
