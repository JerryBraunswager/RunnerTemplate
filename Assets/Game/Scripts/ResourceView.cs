using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private ResourceName _resource;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _inventory.ChangedResource += SetText;
        _text.text = _inventory.GetResourceCount(_resource).ToString();
    }

    private void SetText()
    {
        _text.text = _inventory.GetResourceCount(_resource).ToString();
    }

    private void OnDisable()
    {
        _inventory.ChangedResource -= SetText;
    }
}
