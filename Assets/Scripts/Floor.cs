using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    //script attached to platforms to reset the player's jump count 

    GameObject p1;//the player 
    Player script;//the script attached to the player 

    // Start is called before the first frame update
    void Start()
    {
        p1 = GameObject.Find("Player");
        script = p1.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        script.jumpCount = 0;
    }
}
