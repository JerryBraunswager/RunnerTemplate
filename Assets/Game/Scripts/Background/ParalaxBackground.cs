using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private List<BackgroundMover> backgrounds;
    [SerializeField] private float _length;
    [SerializeField] private float paralaxEffect;

    public float ParalaxEffect => paralaxEffect;
    public float Length => _length;

    private void OnEnable()
    {
        foreach (var background in backgrounds) 
        {
            background.Descend += Move;
        }
    }

    private void OnDisable()
    {
        foreach(var background in backgrounds)
        {
            background.Descend -= Move;
        }
    }

    private void Move(BackgroundMover background)
    {
        background.Move();
    } 
}
