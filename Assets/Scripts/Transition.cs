using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//needed for level and screen transitions
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            //loads test2
            SceneManager.LoadScene(1);
        }
    }
}
