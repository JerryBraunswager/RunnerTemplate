using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInputs))]
public class LinesPool : MonoBehaviour
{
    [SerializeField] private RectTransform _startLine;

    private List<RectTransform> _lines = new List<RectTransform>();
    private PlayerInputs _inputs;

    public event Action<RectTransform> LineChanged;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
    }

    private void Start()
    {
        LineChanged?.Invoke(_startLine);

        for(int i = 0; i < transform.childCount; i++)
        {
            _lines.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }
    }

    private void OnEnable()
    {
        _inputs.Clicked += ChangeLine;
    }

    private void ChangeLine(float obj, CallbackContext context)
    {
        RectTransform result = new RectTransform();

        if (context.performed)
        {
            result = _lines.OrderBy(transform => Math.Abs(obj - Camera.main.ScreenToWorldPoint(transform.position).x)).First();
            
        }

        if(context.canceled)
        {
            result = _startLine;
        }

        LineChanged?.Invoke(result);
    }
}
