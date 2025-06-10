using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _sprite;
    [SerializeField] private ResourceViewSetter _resources;
    [SerializeField] private TMP_Text _stats;

    public void SetView(PurchasableItem item)
    {
        _sprite.sprite = item.Sprite;
        _resources.SetResources(item);
        _stats.text = "";
        Stats[] stats = item.GetStats();

        for (int i = 0; i < stats.Length; i++) 
        {
            if (YandexGame.lang == "ru")
            {
                _stats.text += item.GetName(i);
            }
            else
            {
                _stats.text += stats[i].ToString();
            }

            if(item.GetAmountStat(stats[i]) > 0)
            {
                _stats.text += new string('+', (int)item.GetAmountStat(stats[i]));
            }
            else
            {
                _stats.text += new string('-', Mathf.Abs((int)item.GetAmountStat(stats[i])));
            }

            _stats.text += "\n";
        }

        if(YandexGame.lang == "ru")
        {
            _name.text = item.NameRu + "\nÎ÷êè: " + item.ScoreNeed;
        }
        else
        {
            _name.text = item.name + "\nScore " + item.ScoreNeed;
        }
    }
}
