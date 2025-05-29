using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(JoystickEnabler))]
public class FloatingJoystick : Joystick
{
    private JoystickEnabler _enabler;

    private void Awake()
    {
        _enabler = GetComponent<JoystickEnabler>();
    }

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_enabler.IsEnable == false)
        {
            return;
        }

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}