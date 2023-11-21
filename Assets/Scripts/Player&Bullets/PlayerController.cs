/*****************************************************************************
// File Name : PlayerController.cs
// Author : Isa Luluquisin
// Creation Date : November 9, 2023
//
// Brief Description : This controls funtions like starting the game using the spacebar,
//                     as well as quitting and restarting using esc, 'r', and enter respectively.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("---------- References to Game Objects -------------")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;

    //input actions
    private InputAction move;
    private InputAction rotate;
    private InputAction fire;
    private InputAction restart;
    private InputAction quit;

    [Header("--------------- Boolean Variables ----------------")]
    [Tooltip("Indicates whether game has started")]
    public bool gameIsRunning;
    [Tooltip("Whether play has pressed space for the first time to start the game")]
    public bool spaceWasPressed;
    //whether player is using w/a or up/down arrow keys
    private bool playerIsMoving;
    //whether player is using s/d or left/right arrow keys
    private bool playerIsRotating;

    [Header("Variables relating to player ship")]
    [Tooltip("References player's ship")]
    [SerializeField] private Rigidbody2D ship;
    [Tooltip("speed of rotation")]
    [SerializeField] private float rotSpeed;
    [Tooltip("Speed of forward/backward movement")]
    [SerializeField] private float moveSpeed;

    //which direction the player is trying to move and rotate.
    private float rotDirection; 
    private float moveDirection;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //defines the audiomanager and player ship objects
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        ship = GetComponent<Rigidbody2D>();

        //sets all boolean to false so game isn't running
        gameIsRunning = false;
        spaceWasPressed = false;
        playerIsMoving = false;
        playerIsRotating = false;
        
        //enables action map
        EnableInputs();
    }

    // Update is called once per frame
    void Update()
    {
        //reads directions of player input
        GetDirections();
    }

    private void FixedUpdate()
    {
        //calls on player movement functions only if game is running
        if(gameIsRunning)
        {
            PlayerMovement();
            PlayerRotation();
        }
        
    }

    /// <summary>
    /// This reads the value returned from the player input based on the actionmap
    /// </summary>
    private void GetDirections()
    {
        if (playerIsMoving)
        {
            //1 indicates forward. -1 indicates backwards
            moveDirection = move.ReadValue<float>();
        }
        if (playerIsRotating)
        {
            //1 indicates clockwise direction. -1 indicates counterclockwise
            rotDirection = rotate.ReadValue<float>();
        }
    }

    /// <summary>
    /// When called and playerIsMoving is set to true, the ship moves in the direction and speed for as long
    /// as the player is holding the designated buttons (w/s or up/down arrows)
    /// </summary>
    private void PlayerMovement()
    {
        if (playerIsMoving)
        {
            ship.transform.position += transform.up * Time.deltaTime * moveSpeed * moveDirection;
        }
    }

    /// <summary>
    /// When called and playerIsRotating is set to false, the ship moves in the direction and speed for as long
    /// as the player is holding the designated buttons (a/d or left/right arrows)
    /// </summary>
    private void PlayerRotation()
    {
        if (playerIsRotating)
        {
            ship.transform.Rotate(new Vector3(0, 0, -rotDirection * rotSpeed));
        }
    }

    /// <summary>
    /// When called, a bullet prefab is made by calling on the bullet controller script. The prefab is instantiated
    /// at the same position as the player and the bullet is projected forward. An audio clip is also played.
    /// </summary>
    private void Fire()
    {
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
        audioManager.PlaySFX(audioManager.BulletFired);
    }

    /// <summary>
    /// Handles collisions between the player and other objects. Ensures that player loses a life
    /// when hit by a meteor.
    /// </summary>
    /// <param name="collision">Collision between player and other object<param>
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.transform.tag == "Meteor")
        {
            //plays an audio clip when hit by a meteor
            audioManager.PlaySFX(audioManager.PlayerHit);
            //ensures that player ship isn't moved during collision
            ship.velocity = Vector2.zero;
            //lose a life
            gameManager.PlayerDied();
        }
    }

    #region Input Actions

    /// <summary>
    /// Sets all the player inputs
    /// </summary>
    private void EnableInputs()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("Move");
        rotate = playerInput.currentActionMap.FindAction("Rotate");
        fire = playerInput.currentActionMap.FindAction("Fire");
        restart = playerInput.currentActionMap.FindAction("Restart");
        quit = playerInput.currentActionMap.FindAction("Quit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        rotate.started += Rotate_started;
        rotate.canceled += Rotate_canceled;
        fire.started += Fire_started;
        restart.started += Restart_started;
        quit.started += Quit_started;

    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        playerIsMoving = true;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        playerIsMoving= false;
    }

    private void Rotate_started(InputAction.CallbackContext obj)
    {
        playerIsRotating = true;
    }

    private void Rotate_canceled(InputAction.CallbackContext obj)
    {
        playerIsRotating = false;
    }
    private void Fire_started(InputAction.CallbackContext obj)
    {
        //starts game if game hasn't already started
        if(!spaceWasPressed)
        {
            spaceWasPressed = true;
            gameIsRunning = true;
            gameManager.startScreen.SetActive(false);
            gameManager.inGameText.SetActive(true);
        }
        //calls on fire if game is already running
        else
        {
            Fire();
        }
    }
    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    #endregion

    public void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        rotate.started -= Rotate_started;
        rotate.canceled -= Rotate_canceled;
        fire.started -= Fire_started;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
    }
}
