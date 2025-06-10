using System;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private float _speed;
    private float _size;

    public event Action<BackgroundMover> Descend;

    void Start()
    {
        _speed = gameObject.GetComponentInParent<ParalaxBackground>(true).ParalaxEffect;
        _size = gameObject.GetComponentInParent<ParalaxBackground>(true).Length;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (_speed * Time.deltaTime), transform.position.z);

        if(transform.position.y <= _size * -1)
        {
            Descend?.Invoke(this);
        }
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x, _size, transform.position.z);
    }
}
