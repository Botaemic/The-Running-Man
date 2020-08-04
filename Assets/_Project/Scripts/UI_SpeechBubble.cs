using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpeechBubble : MonoBehaviour
{
    [SerializeField] private List<string> _textList = new List<string>();
    [SerializeField] private GameObject _bubble = null;
    [SerializeField] private Text _textField = null;


    public void SetNewRandomText()
    {
        StartCoroutine(SpeechBubbleDisplay());
    }

    IEnumerator SpeechBubbleDisplay()
    {
        _bubble.SetActive(true);
        _textField.text = _textList[Random.Range(0, _textList.Count)];
        yield return new WaitForSeconds(5);
        _bubble.SetActive(false);
    }
}



