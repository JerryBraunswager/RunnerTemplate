using System;
using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class Score : MonoBehaviour
{
    [SerializeField] private float _scoreIncrese;

    private TMP_Text _text;
    private float _score;

    public event Action<float> ChangedScore;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _score = YandexGame.savesData.Score;
        UpdateText();
    }

    public bool CheckScore(float score)
    {
        return _score >= score;
    }

    public void DecreaseScore(float score) 
    {
        _score -= score;
        UpdateText();
    }

    public void IncreaseScore(Enemy resource, bool result)
    {
        if (result == false) 
        {
            return;
        }

        _score += _scoreIncrese;
        UpdateText();
        ChangedScore?.Invoke(_scoreIncrese);
    }

    private void UpdateText()
    {
        YandexGame.savesData.Score = _score;
        YandexGame.SaveProgress();
        _text.text = _score.ToString();
    }
}
