using System;
using System.Collections;
using UnityEngine;

public class IntroWalkIn : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private Vector3 _startPosition = Vector3.zero;
    [SerializeField] private Vector3 _endPosition = Vector3.zero;
    [SerializeField] private float _lerpSpeed = 2f;

    private Animator _anim = null;

    void Start()
    {
        _player.transform.position = _startPosition;
        _anim = _player.GetComponent<Animator>();
        EventManager.Instance.OnStartIntro += OnStartIntro;
    }

    private void OnStartIntro()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        float distance = Vector3.Distance(_startPosition, _endPosition);
        float startTime = Time.time;

        //First measurement for the check.
        float distanceWalked = (Time.time - startTime) * _lerpSpeed;
        float areWeThereYet = distanceWalked / distance;

        AnimatorSetWalking();

        while (areWeThereYet < 1)
        {
            distanceWalked = (Time.time - startTime) * _lerpSpeed;
            areWeThereYet = distanceWalked / distance;
            _player.transform.position = Vector3.Lerp( _startPosition, _endPosition, areWeThereYet);
            yield return null;
        }

        _anim.SetFloat("Speed_f", 1.0f);
        EventManager.Instance.OnStart?.Invoke();
    }

    private void AnimatorSetWalking()
    {
        _anim.SetFloat("Speed_f", 0.3f);
        _anim.SetBool("Static_b", true);
    }
}