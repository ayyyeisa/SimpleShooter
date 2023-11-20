/*****************************************************************************
// File Name : GameManager.cs
// Author : Isa Luluquisin
// Creation Date : November 20, 2023
//
// Brief Description : This controls the any text or screens appearing in the game
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private AudioManager audioManager;

    public GameObject startScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    public GameObject inGameText;
    private int lives = 3;
    private int score = 0;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        startScreen.SetActive(true);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void PlayerDied()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        
        if(lives == 0)
        {
            audioManager.PlaySFX(audioManager.GameOver);

            endScoreText.text = "Score: " + score;
            loseScreen.SetActive(true);
            inGameText.SetActive(false);
            player.gameIsRunning = false;
        }
    }

    public void UpdateScore(MeteorController meteor)
    {
        //updates score based on size of meteor
        if (meteor.size < 0.75f)
        {
            score += 100;
        }
        else if (meteor.size < 1f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        scoreText.text = "Score: " + score;
    }

}
