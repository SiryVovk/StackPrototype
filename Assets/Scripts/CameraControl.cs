using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private float gameOverMoveDuration = 1f;

    private Coroutine moveRoutine;

    public void MoveUp(float amount)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveUpRoutine(amount));
    }

    public void GameOverCameraMove(int amount)
    {
        if(moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveGameOverRoutien(amount));
    }

    private IEnumerator MoveUpRoutine(float amount)
    {
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = startPos + Vector3.up * amount;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            t = t * t * (3f - 2f * t);

            transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.localPosition = targetPos;
        moveRoutine = null;
    }

    private IEnumerator MoveGameOverRoutien(int amount)
    {
        Camera cam = GetComponent<Camera>();

        float fov = cam.fieldOfView * Mathf.Deg2Rad;

        float distance = (amount + 2) / (2f * Mathf.Tan(fov / 2f));

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos - transform.forward * distance;

        float elapsed = 0f;

        while (elapsed < gameOverMoveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / gameOverMoveDuration);
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        moveRoutine = null;
    }
}
