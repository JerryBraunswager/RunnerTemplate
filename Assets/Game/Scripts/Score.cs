using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class Score : MonoBehaviour
{
    [SerializeField] private float _scoreIncrese;
    private TMP_Text _text;
    private float _score;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _score = YandexGame.savesData.Score;
        UpdateText();
    }

    public void IncreaseScore(Enemy resource, bool result)
    {
        if (result == false) 
        {
            return;
        }

        _score += _scoreIncrese;
        YandexGame.savesData.Score = _score;
        YandexGame.SaveProgress();
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = _score.ToString();
    }
}
