using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballsounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] Vector2 maxVelocity= new Vector2(15f, 15f);
    [SerializeField] Vector2 minVelocity = new Vector2(13f, 13f);

    //States
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //Cached References
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession theGame;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        theGame = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        Debug.Log(myRigidBody2D.velocity);
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    public void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        VelocityTweak();
    }

    private void VelocityTweak()
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(-randomFactor, randomFactor), UnityEngine.Random.Range(-randomFactor, randomFactor));
        if (hasStarted)
        {
            AudioClip clip = ballsounds[UnityEngine.Random.Range(0, ballsounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
            if (theGame.IsVelocityBalanceEnabled())
            {
                VelocityBalance();
            }
        }
    }

    private void VelocityBalance()
    {
        if(myRigidBody2D.velocity.y > maxVelocity.y)
        {
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, maxVelocity.y);
        }

        if(myRigidBody2D.velocity.y < -maxVelocity.y)
        {
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, -maxVelocity.y);
        }

        if (myRigidBody2D.velocity.y < minVelocity.y && myRigidBody2D.velocity.y >= 0)
        {
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, minVelocity.y);
        }

        if (myRigidBody2D.velocity.y > -minVelocity.y && myRigidBody2D.velocity.y < 0)
        {
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, minVelocity.y);
        }

        if (myRigidBody2D.velocity.x > maxVelocity.x)
        {
            myRigidBody2D.velocity = new Vector2(maxVelocity.x, myRigidBody2D.velocity.y);
        }

        if (myRigidBody2D.velocity.x < -maxVelocity.x)
        {
            myRigidBody2D.velocity = new Vector2(-maxVelocity.x, myRigidBody2D.velocity.y);
        }

    }

    public void ResetBallPos()
    {
        hasStarted = false;
    }

}
