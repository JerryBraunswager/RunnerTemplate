using System.Collections.Generic;
using UnityEngine;

public class ResourceViewSetter : MonoBehaviour
{
    [SerializeField] private List<ItemResourceView> _resources;

    public void SetResources(PurchasableItem item)
    {
        foreach (var resource in _resources)
        {
            resource.SetText(item.GetAmountResource(resource.Resource));
        }
    }
}
