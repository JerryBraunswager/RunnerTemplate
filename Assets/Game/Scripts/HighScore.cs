using System;
using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class HighScore : MonoBehaviour
{
    [SerializeField] private Score _score;

    private TMP_Text _text;
    private float _currentScore;

    public event Action<float> ChangedCurrentScore;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        _score.ChangedScore += ChangeScore;
    }

    private void OnDisable()
    {
        _score.ChangedScore -= ChangeScore;
    }

    public void ClearScore()
    {
        _currentScore = 0;
        ChangedCurrentScore?.Invoke(_currentScore);
    }

    private void ChangeScore(float score)
    {
        _currentScore += score;
        ChangedCurrentScore?.Invoke(_currentScore);

        if(_currentScore < YandexGame.savesData.Highscore)
        {
            return;
        }

        YandexGame.savesData.Highscore = _currentScore;
        YandexGame.SaveProgress();
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = YandexGame.savesData.Highscore.ToString();
    }
}

