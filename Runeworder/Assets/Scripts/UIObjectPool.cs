using System.Collections.Generic;
using UnityEngine;

public interface IPoolableUI
{
    void OnBeforeGetFromPool();
    void OnBeforeReleaseToPool();
}

public sealed class UIObjectPool : MonoBehaviour
{
    private static UIObjectPool _instance;

    private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new Dictionary<GameObject, Queue<GameObject>>();
    private readonly Dictionary<GameObject, GameObject> _instanceToPrefab = new Dictionary<GameObject, GameObject>();
    private Transform _poolRoot;

    public static UIObjectPool Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            var host = new GameObject("[UIObjectPool]");
            _instance = host.AddComponent<UIObjectPool>();
            DontDestroyOnLoad(host);
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Prewarm(GameObject prefab, int count, Transform parent = null)
    {
        if (prefab == null || count <= 0)
        {
            return;
        }

        EnsurePool(prefab);
        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(prefab, GetPoolParent(parent));
            TrackInstance(prefab, instance);
            PrepareForPool(instance);
            _pools[prefab].Enqueue(instance);
        }
    }

    public GameObject Get(GameObject prefab, Transform parent = null)
    {
        if (prefab == null)
        {
            return null;
        }

        EnsurePool(prefab);
        GameObject instance;
        if (_pools[prefab].Count > 0)
        {
            instance = _pools[prefab].Dequeue();
        }
        else
        {
            instance = Instantiate(prefab);
            TrackInstance(prefab, instance);
        }

        if (parent != null)
        {
            instance.transform.SetParent(parent, false);
        }

        instance.SetActive(true);
        ResetTransform(instance.transform);
        NotifyBeforeGet(instance);
        return instance;
    }

    public void Release(GameObject instance, Transform poolParentOverride = null)
    {
        if (instance == null)
        {
            return;
        }

        if (!_instanceToPrefab.TryGetValue(instance, out GameObject prefab) || prefab == null)
        {
            Destroy(instance);
            return;
        }

        EnsurePool(prefab);
        NotifyBeforeRelease(instance);
        PrepareForPool(instance);
        instance.transform.SetParent(GetPoolParent(poolParentOverride), false);
        _pools[prefab].Enqueue(instance);
    }

    public bool IsPooledInstance(GameObject instance)
    {
        return instance != null && _instanceToPrefab.ContainsKey(instance);
    }

    private void EnsurePool(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            _pools[prefab] = new Queue<GameObject>();
        }
    }

    private Transform GetPoolParent(Transform fallbackParent)
    {
        if (_poolRoot == null)
        {
            var root = new GameObject("[UIObjectPoolRoot]");
            root.transform.SetParent(transform, false);
            _poolRoot = root.transform;
        }

        return fallbackParent != null ? fallbackParent : _poolRoot;
    }

    private void TrackInstance(GameObject prefab, GameObject instance)
    {
        if (instance == null)
        {
            return;
        }

        _instanceToPrefab[instance] = prefab;
    }

    private static void ResetTransform(Transform target)
    {
        target.localScale = Vector3.one;
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
    }

    private static void PrepareForPool(GameObject instance)
    {
        instance.SetActive(false);
        ResetTransform(instance.transform);
    }

    private static void NotifyBeforeGet(GameObject instance)
    {
        foreach (var poolable in instance.GetComponents<IPoolableUI>())
        {
            poolable.OnBeforeGetFromPool();
        }
    }

    private static void NotifyBeforeRelease(GameObject instance)
    {
        foreach (var poolable in instance.GetComponents<IPoolableUI>())
        {
            poolable.OnBeforeReleaseToPool();
        }
    }
}
