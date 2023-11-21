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
    [Header("Object references for scripts")]
    [SerializeField] private PlayerController player;
    [SerializeField] private AudioManager audioManager;

    [Header("Screens and Texts")]
    [Tooltip("Game instructions that appear before game starts")]
    public GameObject startScreen;
    [Tooltip("Endscore and instructions to restart the game appear")]
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TMP_Text endScoreText;
    [Tooltip("Key instructions to refer throughout game")]
    [SerializeField] private TMP_Text instructionsText;
    [Tooltip("Player's current score")]
    [SerializeField] private TMP_Text scoreText;
    [Tooltip("How many lives player has left")]
    [SerializeField] private TMP_Text livesText;
    [Tooltip("Game object that has children that should be active during gameplay")]
    public GameObject inGameText;

    //number of times player can be hit before game over
    private int lives = 3;
    //score of the player
    private int score = 0;

    private void Start()
    {
        //sets audiomanager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //sets screen to active
        startScreen.SetActive(true);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// When called, this decreases the number of lives the player has and displays it.
    /// If player reaches 0 lives, then the gameOver audio clip is played and playerinput stops
    /// Lose screen is also called to give instructions.
    /// </summary>
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

    /// <summary>
    /// When called, the player's score is updated based on the size of the meteor their bullet hits.
    /// The score is not only updated, but displayed as well.
    /// </summary>
    /// <param name="meteor">the meteor that the bullet hit</param>
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
