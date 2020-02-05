using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //variables to access components 
    Animator animator;
    Transform trans;
    SpriteRenderer spriteRen;

    //rigid body variables 
    float speed = 25;
    float jumpSpeed = 700;
    float fallSpeed = 10;
    Rigidbody2D rb2d;

    //jump variables 
    public int jumpCount = 0;
    const int MAX_JUMP_COUNT = 2;

    //bools for moving and animation
    public bool falling;
    public bool moving;
    bool paused = false;
    Vector2 direction;

    bool goingLeft = false;

    //variables for saving paused states 
    Vector2 pauseVelo;
    float pauseAngleVelo;

    // Start is called before the first frame update
    void Start()
    {
        //gets the components from attached game object
        animator = gameObject.GetComponent<Animator>();
        trans = gameObject.GetComponent<Transform>();
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(direction);

        //changes direction based on bool
        if (goingLeft)
        {
            direction = new Vector2(-1, 0);
        }
        else
        {
            direction = new Vector2(1, 0);
        }

        //flips the direction if the enemy has gone too far 
        if (rb2d.velocity.x > 2 || rb2d.velocity.x < -2)
        {
            goingLeft = !goingLeft;
        }

       
        //moves the enemy
        if (falling)
        {
            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(direction * fallSpeed);
        }
        else
        {
            moving = true;
            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(direction * speed);
        }
    }

    //pauses the enemy
    public void Pause()
    {
        //flips bools to prevent input checks and animation
        paused = !paused;
        animator.enabled = !animator.enabled;

        //pauses the sprite midanimation and stores movement for resuming
        if (paused)
        {
            pauseVelo = rb2d.velocity;
            rb2d.velocity = Vector2.zero;
            pauseAngleVelo = rb2d.angularVelocity;
            rb2d.angularVelocity = 0;
            rb2d.isKinematic = true;
        }
        else
        {
            //resumes sprite motion
            rb2d.velocity = pauseVelo;
            rb2d.angularVelocity = pauseAngleVelo;
            rb2d.isKinematic = false;
        }
    }
}
