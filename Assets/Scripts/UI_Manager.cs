using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Sprite[] liveSprites;

    [SerializeField]
    private Image livesImag;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text restartLevelText;

    [SerializeField]
    private Text ammoText;

    private Game_Manager gameManager;

    public Player player;

    [SerializeField]
    private int _playerAmmo = 15;

    void Start()
    {
        scoreText.text = "Score: " +  0;
        ammoText.text = "Ammo: " + _playerAmmo;
        gameOverText.gameObject.SetActive(false);
        restartLevelText.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();

        if(gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }
        
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString();
        
    }
    public void UpdateLives(int currentLives)
    {
       livesImag.sprite = liveSprites[currentLives];

       if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void DisplayAmmo(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }

    void GameOverSequence()
    {
        gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
       
    }
}
