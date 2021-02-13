using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MessageBoard : MonoBehaviour
{
    public static event Action PlayEvent;

    string currentState = "serve";

    TextMeshPro textBox;
    string mention;
    string other;

    bool instructionsOn = true;

    void OnEnable()
    {
        ScoreBox.ScoreEvent += Scored;
        Paddle.WinEvent += Won;
    }

    void OnDisable()
    {
        ScoreBox.ScoreEvent -= Scored;
        Paddle.WinEvent -= Won;
    }

    void Start()
    {
        textBox = GetComponent<TextMeshPro>();
    }

    string SetMessage(string state, string mention = "", string other = "")
    {
        string message = "";
        if(state == "score")
        {
            message = mention + " Scored! It's " + other + "'s Serve\nPress Enter to Serve";
        }
        else if (state == "won")
        {
            message = mention + " Won!\nPress Enter to Play Again";
        }
        else if (state == "play")
        {
            message = "";
        }
        return message;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Enter"))
        {
            if(currentState == "serve")
            {
                currentState = "play";
                textBox.text = SetMessage("play");
                PlayEvent();
            }
            if(instructionsOn)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                instructionsOn = false;
            }
        }
    }

    void Scored(string scoredOn)
    {
        string mention;
        string other;
        if(scoredOn == "Paddle1")
        {
            mention = "Player 2";
            other = "Player 1";
        }
        else
        {
            mention = "Player 1";
            other = "Player 2";
        }
        if (textBox.text != SetMessage("won", mention))
        {
            textBox.text = SetMessage("score", mention, other);
            textBox.fontSize = 4.5f;
        }
        currentState = "serve";
    }

    void Won(string winner)
    {
        string mention;
        if(winner == "Paddle1")
        {
            mention = "Player 1";
        }
        else
        {
            mention = "Player 2";
        }
        textBox.text = SetMessage("won", mention);
        textBox.fontSize = 4.5f;
        currentState = "serve";
    }
}