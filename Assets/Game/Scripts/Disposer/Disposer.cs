using UnityEngine;

public class Disposer<T> : MonoBehaviour where T : Disposable<T> 
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent(out T obj))
        {
            obj.Invoke(obj, false);
        }
    }
}
