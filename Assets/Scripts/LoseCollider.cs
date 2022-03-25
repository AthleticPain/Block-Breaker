using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    //State variables
    int lives;

    //Cached Reference
    Ball theBall;
    GameSession theGame;

    void Start()
    {
        theBall = FindObjectOfType<Ball>();
        theGame = FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lives = theGame.DecrementNoOfLives();
        if (lives == 0)
        {
            SceneManager.LoadScene("GAME OVER");
        }
        else
        {
            theBall.ResetBallPos();
        }
    }
}