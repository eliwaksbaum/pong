using UnityEngine;
using System;

public class ScoreBox : MonoBehaviour
{
    public static event Action<string> ScoreEvent;
    public string paddle;

    void OnTriggerEnter2D(Collider2D other)
    {

        Ball ball = other.GetComponent<Ball>();
        if(ball != null)
        {
            ball.Reset(paddle);
            ScoreEvent(paddle);
        }
    }
}
