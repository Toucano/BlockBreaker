using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;

    [SerializeField] float power = 10f;
    [SerializeField] float sidePower = 5f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomizer = 0.2f;

    Vector2 paddleToBallVector;

    AudioSource myAudioSource;
    Rigidbody2D rb;

    bool Lauched = false;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Lauched)
        {
            glueBallToPaddle();
            launchBall();
        }
    }
    //Lauching the Ball in all sides
    private void launchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(sidePower , power);
                Lauched = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-sidePower, power);
                Lauched = true;
            }
            else if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2( 0f, power);
                Lauched = true;
            }
                
        } 
    }
    //Let the Ball stick to the paddle
    private void glueBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f,randomizer), Random.Range(0f,randomizer));
        if (Lauched)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            rb.velocity += velocityTweak;
        }
            
    }       
}
