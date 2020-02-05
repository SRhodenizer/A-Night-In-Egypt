using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    //variables for saving paused states 
    Vector2 pauseVelo;
    float pauseAngleVelo;

    // Start is called before the first frame update
    void Start()
    {
        //the animator for the player's sprite
        animator = gameObject.GetComponent<Animator>();
        //transform for the player 
        trans = gameObject.GetComponent<Transform>();
        //gets the rigid body
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        //sprite render for the player 
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        falling = CheckFalling();
        if (!paused)
        {
            CheckInput();
        }
        moving = false;
    }

   //gets what keys are pressed down
   public void CheckInput() {

        //gets a float value for velocity based on input's get axis
        float velocity = Input.GetAxis("Horizontal") * speed;
        
        if (velocity < 0)//move left
        {
            //gets the sprite facing the right direction
            spriteRen.flipX = true;

            if (falling)
            {
                Vector2 movement = new Vector2(-1, 0);
                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
                rb2d.AddForce(movement * fallSpeed);
            }
            else
            {
                moving = true;
                //switch run animation
                animator.Play("Run");
                //moves the game object
                Vector2 movement = new Vector2(-1, 0);
                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
                rb2d.AddForce(movement * speed);
            }
        }
        else if(velocity > 0) //move right
        {
            //gets the sprite facing the right direction
            spriteRen.flipX = false;

            if (falling)
            {
                Vector2 movement = new Vector2(1, 0);
                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
                rb2d.AddForce(movement * fallSpeed);
            }
            else
            {
                moving = true;
                //switch run animation
                animator.Play("Run");
                //moves the game object
                Vector2 movement = new Vector2(1, 0);
                //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
                rb2d.AddForce(movement * speed);
            }
        }

       //jump for keyboard and controller 
       if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick 1 button 0")) {

           if(jumpCount < MAX_JUMP_COUNT)
           {
               //moves the game object
               Vector2 movement = new Vector2(0, 1);
               //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
               rb2d.AddForce(movement * jumpSpeed);
           }
            jumpCount++;
        }
       else
       //plays the correct jump animations 
        if (falling && rb2d.velocity.y > 0)
        {
            animator.Play("Jump");
        }
        else
        if (falling && rb2d.velocity.y < 0)
        {
            animator.Play("Falling");
        }

        //if not moving idle
        if (!falling && !moving) {
            animator.Play("Idle");
        }
    }

    //constantly checks if the player is falling 
    bool CheckFalling() {
        bool result = false;

        if(rb2d.velocity.y != 0)
        {
            result = true;
        }
        return result;
    }

    //pauses the character
    public void Pause() {
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
        else {
            //resumes sprite motion
            rb2d.velocity = pauseVelo;
            rb2d.angularVelocity = pauseAngleVelo;
            rb2d.isKinematic= false;
        }


    }
}
