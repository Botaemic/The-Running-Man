using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void OnContinuePressed()
    {
        //Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnRestartPressed()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuPressed()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
