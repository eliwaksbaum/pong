using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Paddle : MonoBehaviour
{
    public float paddleSpeed = 0.3f;
    //public float bounce = 20;
    public string axis;
    public TextMeshPro scoreText;

    float vertical;
    bool active = false;
    int score= 0;

    SpriteRenderer rend;
    float halfheight;

    public static event Action<string> WinEvent;

    void OnEnable()
    {
        ScoreBox.ScoreEvent += Scored;
        MessageBoard.PlayEvent += Play;
        WinEvent += Lost;
    }

    void OnDisable()
    {
        ScoreBox.ScoreEvent -= Scored;
        MessageBoard.PlayEvent += Play;
        WinEvent -= Lost;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        halfheight = rend.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw(axis);
    }

    void FixedUpdate()
    {
        if (active)
        {
            float currentY = gameObject.transform.position.y;

            if (vertical > 0 && currentY < (5 - halfheight))
            {
                gameObject.transform.Translate(new Vector3(0, vertical * paddleSpeed, 0));
            }
            else if (vertical < 0 && currentY > (-5 + halfheight))
            {
                gameObject.transform.Translate(new Vector3(0, vertical * paddleSpeed, 0));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            // float yComp = bounce * UnityEngine.Random.Range(-.8f, .8f);
            // Vector2 force = new Vector2(0, yComp);
            // ball.AddForce(force);

            ball.AdjustBounce();
        }
    }

    void Scored(string scoredOn)
    {
        active = false;
        if (scoredOn != axis)
        {
            score += 1;
            if(score == 10)
            {
                WinEvent(axis);
                score = 0;
                transform.position = new Vector3(transform.position.x, 0, 0);
                StartCoroutine(DoADance());
            }
            scoreText.text = score.ToString();
        }
    }

    void Lost(string winner)
    {
        if (winner != axis)
        {
            score = 0;
            scoreText.text = score.ToString();
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    IEnumerator DoADance()
    {
        int i = 0;
        while(i < 16)
        {
            if(rend.color.a > 0)
            {
                rend.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                rend.color = new Color(1f, 1f, 1f, 1f);
            }
            i += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Play()
    {
        active = true;
    }
}