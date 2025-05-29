using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour 
{
    [SerializeField] private T _prefab;

    public T Prefab => _prefab;

    private Queue<T> _pool;

    public IEnumerable<T> PooledObjects => _pool;
    public event Action<T> Spawned;

    private void Awake()
    {
        _pool = new Queue<T>();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            var obj = Instantiate(_prefab);
            obj.transform.SetParent(transform, false);
            Spawned?.Invoke(obj);
            return obj;
        }

        _pool.Peek().gameObject.SetActive(true);
        Spawned?.Invoke(_pool.Peek());
        return _pool.Dequeue();
    }

    public void PutObject(T obj)
    {
        obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
