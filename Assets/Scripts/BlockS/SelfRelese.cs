using System.Collections;
using UnityEngine;

public class SelfRelese : MonoBehaviour
{
    private float timeToDestruction = 5f;

    private CutBlocksPool cutBlocksPool;

    private Coroutine lifeRoutine;

    public void Init(CutBlocksPool cutBlocksPool)
    {
        this.cutBlocksPool = cutBlocksPool;
    }


    private void OnEnable()
    {
        lifeRoutine = StartCoroutine(LifeTimer());
    }

    private void OnDisable()
    {
        if (lifeRoutine != null)
            StopCoroutine(lifeRoutine);
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(timeToDestruction);
        cutBlocksPool.Relese(this);
    }
}
