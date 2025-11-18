using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemEffect itemEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectItem();

            SpawnEffect();

            PoolManager.Instance.ReturnPool(this);
        }
    }

    private void SpawnEffect()
    {
        if (itemEffectPrefab != null && PoolManager.Instance != null)
        {
            // 풀에서 이펙트 가져오기
            ItemEffect effect = PoolManager.Instance.GetFromPool(itemEffectPrefab);
            if (effect != null)
            {
                effect.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }
        }
    }

}
