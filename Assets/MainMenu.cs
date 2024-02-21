using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void startGame()
    {
        
        SceneManager.LoadScene(1); //loads  the scene of the main game world
        World.worldSeed = Random.Range(0, 100000);       
        
    }
}
