using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    [SerializeField] private float _bounceForce = 5f;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    public AudioClip moneySound;
    public AudioClip explodeSound;

    
    //private AudioSource playerAudio;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    private Vector3 _currentGravity = Vector3.zero;


    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        _currentGravity = Physics.gravity;
        Physics.gravity *= gravityModifier;

        //playerAudio = GetComponent<AudioSource>();
        
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    private void OnDestroy()
    {
        Physics.gravity = _currentGravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.IsGameRunning)
        {
            // While space is pressed and player is low enough, float up
            if (Input.GetKey(KeyCode.Space) && !gameOver)
            {
                playerRb.AddForce(Vector3.up * floatForce);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Boundary boundary = other.gameObject.GetComponent<Boundary>();
        if (boundary) 
        {
            playerRb.AddForce(Vector3.up * _bounceForce);
            return;
        }

        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            //playerAudio.PlayOneShot(explodeSound, 1.0f);
            AudioManager.Instance.PlaySound(explodeSound, transform.position, false);
            gameOver = true;
            EventManager.Instance.OnGameOver?.Invoke();
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            //playerAudio.PlayOneShot(moneySound, 1.0f);
            AudioManager.Instance.PlaySound(moneySound, transform.position, false);
            Destroy(other.gameObject);

        }

    }

}
