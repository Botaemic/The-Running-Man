using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum IdleAnimation
{
    Idle_CrossedArms = 1,
    Idle_HandsOnHips,
    Idle_CheckWatch,
    Salute = 6,
    Idle_SittingOnGround = 9
}


public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _uiCharacter = null;
    [SerializeField] private float _idleAnimationTimer = 2f;

    [SerializeField] private UI_SpeechBubble _speechBubble = null;

    private Animator _uiCharAnimator = null;

    void Start()
    {
        _uiCharAnimator = _uiCharacter.GetComponent<Animator>();
        _uiCharAnimator.SetFloat("Speed_f", 0f);
        StartCoroutine(IdleAnimFactory());
    }

    public void OnRunnerPressed()
    {
        SceneManager.LoadScene(1); // BAD programmer....
    }

    public void OnBalloonsPressed()
    {
        SceneManager.LoadScene(2);
    }


    #region Animations
    IEnumerator IdleAnimFactory()
    {
        while(true) // gets destroyed anyways when loading new scene
        {
            yield return new WaitForSeconds(_idleAnimationTimer);
            _speechBubble?.SetNewRandomText();
            var newAnim = (int)GetRandomIdleAnimation();
            AnimatorSetAnimation(newAnim);
            //yield return new WaitForSeconds(1f);
            //AnimatorSetAnimation(0);
        }
    }

    private IdleAnimation GetRandomIdleAnimation()
    {
        var values = Enum.GetValues(typeof(IdleAnimation));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (IdleAnimation)values.GetValue(random);
    }

    private void AnimatorSetAnimation(int animationId)
    {
        _uiCharAnimator.SetInteger("Animation_int", animationId);
    }
    #endregion
}
