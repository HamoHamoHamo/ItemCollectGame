using System.Collections;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    private float returnTime = 5f;
    private Coroutine returnCoroutine;

    private void OnEnable()
    {
        // 활성화될 때마다 코루틴 시작
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(ReturnToPoolAfterDelay());
    }

    private void OnDisable()
    {
        // 비활성화될 때 코루틴 정지
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(returnTime);
        PoolManager.Instance.ReturnPool(this);
    }
}