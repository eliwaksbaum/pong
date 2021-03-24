using UnityEngine;

public class Ball : MonoBehaviour
{
    int serve = 1;
    public float speed = 100f;

    new Rigidbody2D rigidbody;

    void OnEnable()
    {
        MessageBoard.PlayEvent += Serve;
    }

    void OnDisable()
    {
        MessageBoard.PlayEvent -= Serve;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Serve()
    {
        float yComp = 0;//Random.Range(-0.8f, 0.8f);
        Vector2 dir = new Vector2(serve, yComp);
        Vector2 force = dir.normalized * speed;
        rigidbody.AddForce(force);
    }

    public void Reset(string scoredOn)
    {
        gameObject.transform.position = Vector3.zero;
        rigidbody.velocity = Vector2.zero;

        switch(scoredOn)
        {
            case "Paddle1":
                serve = 1;
                break;
            case "Paddle2":
                serve = -1;
                break;
            default:
                Debug.Log("invalid argument on Ball.Reset()");
                break;
        }
    }

    // public void AddForce(Vector2 force)
    // {
    //     rigidbody.AddForce(force);
    // }

    public void AdjustBounce()
    {
        float adjustment = Random.Range(-1.4f, 1.4f);
        float vY = rigidbody.velocity.y;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, vY + adjustment);
    }
}
