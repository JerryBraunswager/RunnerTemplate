using TMPro;
using UnityEngine;

public class ItemResourceView : MonoBehaviour
{
    [SerializeField] private ResourceName _resource;
    [SerializeField] private TMP_Text _text;

    public ResourceName Resource => _resource;

    public void SetText(int amount)
    {
        gameObject.SetActive(false);

        if (amount > 0)
        {
            _text.text = amount.ToString();
            gameObject.SetActive(true);
        }
    }
}
