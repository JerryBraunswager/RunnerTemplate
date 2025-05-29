using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _character;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _character.HealthChanged += SetText;
    }

    private void OnDisable()
    {
        _character.HealthChanged -= SetText;
    }

    private void SetText(float health)
    {
        _text.text = health.ToString();
    }
}
