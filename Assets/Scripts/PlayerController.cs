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
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;
    private InputAction rotate;
    private InputAction restart;
    private InputAction quit;

    private bool playerIsMoving;
    private bool playerIsRotating;
    [SerializeField] private Rigidbody2D ship;

    [SerializeField] private float revSpeed;
    [SerializeField] private float moveSpeed;
    private int revDirection; //1 indicates clockwise direction. -1 indicates counterclockwise
    private float moveDirection;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        EnableInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }

    private void FixedUpdate()
    {
        if (playerIsMoving)
        {
            print("Paddle Should Be Moving");
            ship.velocity = new Vector2(0, moveSpeed * moveDirection);
        }
        else
        {
            print("Paddle Should Not Be Moving");
            ship.velocity = Vector2.zero;
        }
    }

    #region Input Actions
    private void EnableInputs()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("Move");
        rotate = playerInput.currentActionMap.FindAction("Rotate");
        restart = playerInput.currentActionMap.FindAction("Restart");
        quit = playerInput.currentActionMap.FindAction("Quit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        rotate.started += Rotate_started;
        rotate.canceled += Rotate_canceled;
        restart.started += Restart_started;
        quit.started += Quit_started;

    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        Debug.Log("sprite should be moving");
        playerIsMoving = true;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("sprite should no longer be moving");
        playerIsMoving= false;
    }

    private void Rotate_started(InputAction.CallbackContext obj)
    {
        Debug.Log("sprite should be rotating");
        playerIsMoving = true;
    }

    private void Rotate_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("sprite should've stopped rotating");
        playerIsMoving = false;
    }
    private void Restart_started(InputAction.CallbackContext obj)
    {
        Debug.Log("scene shouldve been reloaded");
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
        restart.started -= Restart_started;
        quit.started -= Quit_started;
    }
}
