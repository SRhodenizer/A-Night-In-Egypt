using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManagment : MonoBehaviour
{

    //objects in the scene
    GameObject[] objects;

    //Player Script
    Player p1;

    //enemy scripts
    List<Enemy> enemies;

    //pausing the game 
    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //gets all the gameobjects in a scene
         objects = GameObject.FindObjectsOfType<GameObject>();

        //instantiates the enemy list
        enemies = new List<Enemy>();

        //sorts through the objects and saves the Player script and enemy scripts
        foreach (GameObject ob in objects)
        {
            //saves the player script
            if (ob.name == "Player")
            {
                p1 = ob.GetComponent<Player>();
            }

            //saves the enemy scripts
            if (ob.tag == "Enemy")
            {
                enemies.Add(ob.GetComponent<Enemy>());
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //button input for pausing the game 
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown("joystick button 7"))
        {
            isPaused = !isPaused;
            PauseGame();
        }
        
        
    }

    //does GUI Labels 
    void OnGUI()
    {
        //Pause Menu
        if (isPaused)
        {
            GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
        }
    }

    //method for pausing the GameObjects in the scene
    void PauseGame() {
        //pauses the player
        p1.Pause();

        //pauses the enemies
        foreach (Enemy enemy in enemies) {
            enemy.Pause();
        }
    }


    //methods that pause the game when tabbed out
    private void OnApplicationFocus(bool focus)
    {
        isPaused = !focus;
    }

   
    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
    }
}
