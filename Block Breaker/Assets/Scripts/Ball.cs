using UnityEngine;

public class Ball : MonoBehaviour {

    // config params
    [SerializeField] float xRandomLaunchRange = 0.5f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactorRange = 0.2f;

    // state
    Vector3 paddleToBallVector;
    bool hasStarted = false;
    float xDirection; //
    float yDirection;
    float startMagnitude;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


	// Use this for initialization
	void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

        xDirection = Random.Range(-xRandomLaunchRange, xRandomLaunchRange); // Generate random launch value
        if (Mathf.Approximately(xDirection, 0)) // Compare Approximately if it is close to 0
        {
            xDirection = xRandomLaunchRange; // if so, we give it the default value because a direction can't be 0 or it won't move.
        }
        // The magnitude is : |a| = sqrt(x²+y²), we made x random, now we need to calculate y to keep same magnitude.
        // by the reverse operation
        yDirection = Mathf.Sqrt(Mathf.Pow(GameSession.instance.ballSpeed,2) + Mathf.Pow(xDirection,2));

        float y = transform.position.y - GameObject.FindWithTag("Paddle").transform.position.y;
        paddleToBallVector = new Vector3(0,y,0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            Launch();
        }
        Debug.Log(myRigidBody2D.velocity.magnitude);

    }
    private void FixedUpdate()
    {
        if (hasStarted)
        {
            // After each collision with the paddle, the ball loses some of it's velocity. 
            // this code to ensure that it never goes below 97.5% of it's speed, and to never exceed 102.5% after recalculating
            // this is a quick fix, til someone can figure a better alternative
            if(myRigidBody2D.velocity.magnitude * 0.985f < startMagnitude || myRigidBody2D.velocity.magnitude * 1.015f > startMagnitude)
                myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * startMagnitude;
        }
        
    }
    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xDirection, yDirection) * GameSession.instance.ballSpeed;
            startMagnitude = myRigidBody2D.velocity.magnitude;
        }
    }

    private void LockBallToPaddle()
    {
        transform.position = GameObject.FindWithTag("Paddle").transform.position + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Paddle"))
        {
            Vector2 velocityTweak = new Vector2 (Random.Range(-randomFactorRange, randomFactorRange), 0);
            myRigidBody2D.velocity += velocityTweak;
        }
        
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }

}