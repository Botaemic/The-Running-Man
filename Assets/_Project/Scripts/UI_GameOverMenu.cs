using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text _scoreText = null;

    private void OnEnable()
    {
        _scoreText.text = LevelManager.Instance.Score.ToString();
    }

    private void OnDisable()
    {
        _scoreText.text = "";
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
