using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    public Transform Root { get; private set; }

    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        Root = new GameObject($"{prefab.name}_pool").transform;

        for (int i = 0; i < initCount; i++)
        {
            var inst = Object.Instantiate(prefab, Root);
            inst.name = prefab.name;
            inst.gameObject.SetActive(false);
            pool.Enqueue(inst);
        }
    }

    public T Dequeue()
    {
        if (pool.Count == 0) return null;

        var inst = pool.Dequeue();
        inst.gameObject.SetActive(true);

        return inst;
    }

    public void Enqueue(T instance)
    {
        if (instance == null) return;

        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }

    public void Clear()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            if (obj != null)
            {
                Object.Destroy(obj.gameObject);
            }
        }

        pool.Clear();
    }
}
