using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickEnabler : MonoBehaviour
{
    [SerializeField] private bool _isEnable;

    public bool IsEnable => _isEnable;
}
