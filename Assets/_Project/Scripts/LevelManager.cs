using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Lazy Singelton
    private static LevelManager _instance = null;

    // Lazy singleton
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<LevelManager>();
                singletonObject.name = typeof(LevelManager).ToString() + " (Singleton)";
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private int _score = 0;
    [Range(0,1)]
    [SerializeField] private float _scoreFactor = .5f;
    [Header("Menu's")]
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _gameOverMenu = null;
    [SerializeField] private GameObject _gameMenu = null;
    [SerializeField] private Text _scoreText = null;

    [Header("Background Music")]
    [SerializeField] private AudioClip _backgroundMusic;

    [Space]
    [SerializeField] private bool _isGameRunning = false;
    [SerializeField] private float _standardGameSpeed = 5f;

    #region Properties
    public bool IsGameRunning { get => _isGameRunning; private set => _isGameRunning = value; }
    private float _gameSpeed = 5f;
    public float GameSpeed { get => _gameSpeed; set => _gameSpeed = value; }
    public int Score { get => _score; }
    #endregion

    private bool _isDashing = false;
    private float _scoreCounter = 0f;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GameSpeed = _standardGameSpeed;

        EventManager.Instance.OnGameOver += OnGameOver;
        EventManager.Instance.OnStart += OnStart;
        EventManager.Instance.OnDash += OnDash;
    }

    private void OnDash(float multiplier)
    {
        if (_isDashing) { return; }
        
        StartCoroutine(StartDash(multiplier));
    }
    
    private IEnumerator StartDash(float multiplier)
    {
        _isDashing = true;
        GameSpeed = GameSpeed * multiplier;
        yield return new WaitForSeconds(0.2f); //todo remove the MAGIC number
        GameSpeed = _standardGameSpeed;
        _isDashing = false;
        EventManager.Instance.OnEndDash?.Invoke();
        yield return null;
    }

    private void OnStart()
    {
        IsGameRunning = true;
        AudioManager.Instance.PlayBackgroundMusic(_backgroundMusic, Vector3.zero, true);
        _scoreCounter = 0f;
        Time.timeScale = 1f;
        _gameMenu.SetActive(true);
    }

    private void OnGameOver()
    {
        IsGameRunning = false;
        _gameMenu.SetActive(false);
        _gameOverMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }

        if (IsGameRunning) //Ugly
        {
            _scoreCounter += (GameSpeed * Time.deltaTime * _scoreFactor);
            _score = (int)_scoreCounter;
            _scoreText.text = _score.ToString();
        }
    }
}
