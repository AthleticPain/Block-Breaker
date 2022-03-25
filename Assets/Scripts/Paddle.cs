using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    //Cached References
    GameSession theGame;
    Ball theBall;

    // Start is called before the first frame update
    void Start()
    {
        theGame = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if(theGame.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            return mousePosInUnits;
        }
    }
}
