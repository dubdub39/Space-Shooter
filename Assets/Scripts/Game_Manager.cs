using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    private void Update()
    {
   
       if(Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene
        }

       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

}
