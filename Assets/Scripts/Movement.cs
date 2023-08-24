using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Movement : MonoBehaviour
{
    public static Movement instance { get; private set; }
    [SerializeField]
    private float translationSpeed;
    private const float MIN_X = -9.70f;
    private const float MAX_X = 9.70f;
    private const float MIN_Y = 0.1f;
    private const float MAX_Y = 2;
    [SerializeField]
    private float downSpeed;

    [SerializeField]
    private float moveSpeed = 0.3f;
    [SerializeField]
    private float currentSpeed;
    private float speedUpPercent = 0.3f;
    private float multipleOf500 = 0;

    private AudioSource audioSource;
    public AudioClip collisionSound;

    private bool isSlowedDown = false;
    private float slowdownStartTime;
    private float normalMoveSpeed;

    private ParticleSystem collisionParticles;






    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0.1f, 0);

        audioSource = GetComponent<AudioSource>();
        collisionParticles = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        


        //limite position Vertical
        if(transform.position.y > MAX_Y)
        {
            transform.position = new Vector3(transform.position.x, MAX_Y, transform.position.z);
        }
        if(transform.position.y < MIN_Y)
        {
            transform.position = new Vector3(transform.position.x, MIN_Y, transform.position.z);
        }

        float verticalMove = Input.GetAxis("Vertical") * translationSpeed * Time.deltaTime;
        transform.position += transform.up * verticalMove;

        if(verticalMove == 0 && transform.position.y > MIN_Y)
        {
            transform.position -= downSpeed * transform.up * Time.deltaTime;
        }
        //limite position Horizontal
        if (transform.position.x > MAX_X)
        {
            transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
        }
        if (transform.position.x < MIN_X)
        {
            transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);
        }

        float horizontalMove = Input.GetAxis("Horizontal") * translationSpeed * Time.deltaTime;
        transform.position += transform.right * horizontalMove;

       //Deplacement vers l'avant
            currentSpeed += moveSpeed * Time.deltaTime;

        int currentMultipleOf500 = Mathf.FloorToInt(GameController.Instance.Score / 500);

        if (currentMultipleOf500 > multipleOf500)
        {
            multipleOf500 = currentMultipleOf500;
            float accelerate = moveSpeed * speedUpPercent;
            moveSpeed += accelerate;
        }


       if(currentSpeed > moveSpeed)
        {
            currentSpeed = moveSpeed;
        }


        if (!isSlowedDown)
        {
            transform.position += transform.forward * currentSpeed;
        }
        else
        {
            float timeSinceSlowdown = Time.time - slowdownStartTime;
            if(timeSinceSlowdown < 0.1f)
            {
                transform.position += transform.forward * currentSpeed * 0.5f;
            }
            else
            {
                isSlowedDown = false;
                moveSpeed = normalMoveSpeed; // Rétablir la vitesse normale
                ResetTimeScale();
            }
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (GameController.Instance.Health > 0)
        {
            GameController.Instance.Health -= 10;

            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
                isSlowedDown = true;
                normalMoveSpeed = moveSpeed;
                moveSpeed = 0.1f;
                ResetMoveSpeed();
                ResetTimeScale();
                slowdownStartTime = Time.time; // Enregistrer le temps de début du ralentissement
            }

            if (collisionParticles != null)
            {
                collisionParticles.Play();
            }

            if (GameController.Instance.Health <= 0)
            {
                
                GameOver();
            }
            
        }
        
    }

    private void GameOver()
    {
       
        SceneManager.LoadScene("GameOver");
    }

   private void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

   private void ResetMoveSpeed()
    {
        moveSpeed = 0.3f;
    }

    
}
