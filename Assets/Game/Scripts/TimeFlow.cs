using UnityEngine;

public class TimeFlow : MonoBehaviour
{
    [SerializeField] private RectTransform _startMenu;
    private bool _isGamePlay = false;

    public bool isGamePlay => _isGamePlay;

    public void StopGame()
    {
        _isGamePlay = false;
    }

    public void StartGame()
    {
        _startMenu.gameObject.SetActive(false);
        _isGamePlay = true;
    }
}
