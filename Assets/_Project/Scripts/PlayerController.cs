using Botaemic.Unity.BarSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxStaminapoints = 10f;
    [SerializeField] private Bar _staminaBar = null;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private ParticleSystem _dirtParticle = null;

    [SerializeField] private AudioClip _jumpSound = null;
    [SerializeField] private AudioClip _crashSound = null;
    [SerializeField] private AudioClip _drinkSound = null;

    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private float _speedIncreaseInterval = 10f;
    [Header("Speech settings")]
    [SerializeField] private UI_SpeechBubble _speech = null;
    [SerializeField] private float _maximumSpeechTime = 10f;
    [SerializeField] private float _minimumSpeechTime = 5f;

    private int _nrOfjumps = 0;

    private Rigidbody _rb = null;
    private bool _isGrounded = true;
    private Animator _animator = null;
    [Header("Debug Mode")]
    [SerializeField] private bool _debugMode;

    private Stat _stamina = null;
    private bool _isJumping = false;

    private float _speedIncreaseTimer = 0f;
    private float _speechTimer = 0f;
    private Vector3 _currentGravity = Vector3.zero;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _currentGravity = Physics.gravity;
        Physics.gravity *= _gravityModifier;

        LevelManager.Instance.GameSpeed = _playerSpeed;
        
        AnimatorSetSpeed(0);

        EventManager.Instance.OnGameOver += OnGameOver;
        EventManager.Instance.OnStart += OnStart;
        EventManager.Instance.OnPause += OnPause;

        _stamina = new Stat(_maxStaminapoints);
        if (_staminaBar) { _staminaBar.Initialize(_stamina); }

        _speedIncreaseTimer = _speedIncreaseInterval;
        _speechTimer = UnityEngine.Random.Range(_minimumSpeechTime, _maximumSpeechTime);
    }

    private void Update()
    {
        if (LevelManager.Instance.IsGameRunning)
        {
            if (_isGrounded && !_dirtParticle.isPlaying) { _dirtParticle.Play(); }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
            }

            if (Input.GetKey(KeyCode.E) && _stamina.CurrentValue > Mathf.Epsilon)
            {
                LevelManager.Instance.GameSpeed = 4 * _playerSpeed; //todo Change
                _stamina.AddPoints(-3 * Time.deltaTime);
            }
            else
            {
                LevelManager.Instance.GameSpeed = _playerSpeed; //todo Change
            }

            IncreaseSpeed();
            SpeechTime();
        }
    }

    private void SpeechTime()
    {
        _speechTimer -= Time.deltaTime;
        if (_speechTimer <= Mathf.Epsilon)
        {
            _speech?.SetNewRandomText();
            _speechTimer = UnityEngine.Random.Range(_minimumSpeechTime, _maximumSpeechTime);
        }
    }

    void FixedUpdate()
    {
        if (LevelManager.Instance.IsGameRunning)
        {
            if (_isJumping) {  Jump(); }
        }
    }

    private void Jump()
    {
        if (_isGrounded) { _nrOfjumps = 0; }

        if(_isGrounded || _nrOfjumps < _maxJumps)
        {
            _nrOfjumps++;
            _rb.velocity = Vector3.zero;    // Cahracter isn't moving so Vector3.zero should be enough
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            
            AudioManager.Instance.PlaySound(_jumpSound, transform.position, false);
            _dirtParticle.Stop();
            AnimatorJump();
            _isGrounded = false;
        }
        _isJumping = false;
    }

    private void OnDestroy()
    {
        Physics.gravity = _currentGravity;
    }

    public void IncreaseSpeed()
    {
        _speedIncreaseTimer -= Time.deltaTime;
        if (_speedIncreaseTimer <= Mathf.Epsilon)
        {
            _speedIncreaseTimer = _speedIncreaseInterval;
            _playerSpeed *= 1.2f;
        }
    }


    #region Eventhandlers
    private void OnGameOver()
    {
        Debug.Log("Trigger Death");
        AnimatorSetSpeed(0);
        _animator.SetInteger("DeathType_int", 1);
        _animator.SetBool("Death_b", true);
    }

    private void OnPause()
    {
        AnimatorSetSpeed(0);
    }

    private void OnStart()
    {
        AnimatorSetSpeed(1);
    }
    #endregion

    #region Animations

    private void AnimatorSetSpeed(float speed)
    {
        _animator.SetFloat("Speed_f", speed);
    }

    private void AnimatorSetAnimation(int animationId)
    {
        _animator.SetInteger("Animation_int", animationId);
    }

    private void AnimatorJump()
    {
        _animator.SetTrigger("Jump_trig");
    }

    private IdleAnimation GetRandomIdleAnimation()
    {
        var values = Enum.GetValues(typeof(IdleAnimation));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (IdleAnimation)values.GetValue(random);
    }
    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        Ground ground = collision.gameObject.GetComponent<Ground>();
        if (ground)
        {
            _isGrounded = true;
            return;
        }

        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle && !_debugMode)
        {
            _dirtParticle.Stop();
            AudioManager.Instance.PlaySound(_crashSound, transform.position, false);
            EventManager.Instance.OnGameOver?.Invoke();
            return;
        }

        Energy energy = collision.gameObject.GetComponent<Energy>();
        if (energy)
        {
            Debug.Log("Energy" );
            AudioManager.Instance.PlaySound(_drinkSound, transform.position, false);
            _stamina.AddPoints(energy.EnergyPoints);
            Destroy(collision.gameObject);
            return;
        }


    }
#endregion
}
