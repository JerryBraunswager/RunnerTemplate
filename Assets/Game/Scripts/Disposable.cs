using System;
using UnityEngine;

public class Disposable<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action<T, bool> Disposed;

    public void Invoke(T obj, bool result)
    {
        transform.localScale = Vector3.one;
        Disposed?.Invoke(obj, result);
    }
}
