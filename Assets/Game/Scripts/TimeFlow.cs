using UnityEngine;

public class TimeFlow : MonoBehaviour
{
    [SerializeField] private RectTransform _startMenu;
    [SerializeField] private RectTransform _endMenu;
    [SerializeField] private HighScore _highScore;
    private bool _isGamePlay = false;

    public bool isGamePlay => _isGamePlay;

    public void StopGame()
    {
        _endMenu.gameObject.SetActive(true);
        _isGamePlay = false;
    }

    public void StartGame()
    {
        _startMenu.gameObject.SetActive(false);
        _endMenu.gameObject.SetActive(false);
        _highScore.ClearScore();
        _isGamePlay = true;
    }
}
