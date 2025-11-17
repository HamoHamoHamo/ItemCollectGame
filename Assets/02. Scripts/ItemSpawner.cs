using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] private Item itemPrefab;
    [SerializeField] private ItemEffect itemEffectPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int initialSpawnCount = 5;
    // [SerializeField] private int maxItemCount = 10;
    [SerializeField] private float spawnInterval = 5.0f;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaCenter = Vector3.zero;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);

    [Header("Height Settings")]
    [SerializeField] private float spawnHeight = 1.0f;

    private void Awake()
    {
        SetupObjectPool();
    }

    private void Start()
    {
        SpawnInitialItems();
        StartCoroutine(SpawnRoutine());
    }

    private void SetupObjectPool()
    {
        if (itemPrefab != null)
        {
            PoolManager.Instance.CreatePool(itemPrefab, initialSpawnCount + 10, transform);
        }
        if (itemEffectPrefab != null)
        {
            PoolManager.Instance.CreatePool(itemEffectPrefab, initialSpawnCount + 10, transform);
        }

    }

    private void SpawnInitialItems()
    {
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnItem();
        }
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);
        while (true)
        {
            yield return wait;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (itemPrefab == null) return;

        var itemComponent = itemPrefab.GetComponent<Item>();
        if (itemComponent == null) return;

        var item = PoolManager.Instance.GetFromPool(itemComponent);
        if (item == null) return;

        Vector3 randomPosition = GetRandomSpawnPosition();

        item.transform.position = randomPosition;
        item.transform.rotation = Quaternion.identity;
        item.gameObject.SetActive(true);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        Vector3 spawnPosition = spawnAreaCenter + new Vector3(randomX, spawnHeight, randomZ);
        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter + Vector3.up * spawnHeight, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.z));
    }
}
