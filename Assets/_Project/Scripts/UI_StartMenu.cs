using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartMenu : MonoBehaviour
{
    public void OnStartPressed()
    {
        //EventManager.Instance.OnStart?.Invoke();
        EventManager.Instance.OnStartIntro?.Invoke();
        gameObject.SetActive(false); ;
    }
    
    public void OnBalloonStartPressed()
    {
        EventManager.Instance.OnStart?.Invoke();
        gameObject.SetActive(false); ;
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
