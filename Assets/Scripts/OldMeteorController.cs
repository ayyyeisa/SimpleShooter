/*****************************************************************************
// File Name : MeteorController.cs
// Author : Isa Luluquisin
// Creation Date : November 19, 2023
//
// Brief Description :  This controls the spawning
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class OldMeteorController : MonoBehaviour
{
    public PlayerController playerInstance;
    [SerializeField] private GameObject[] meteors;
    private GameObject meteor;
    public Coroutine MeteorLaunch;
    private float waitTime;

    private float randomVecX;
    private float randomVecY;
    private Vector2 randomVec;

    void Update()
    {
        if (!playerInstance.gameIsRunning)
        {
            GameStartCheck();
        }
        else if (playerInstance.gameIsRunning && playerInstance.spaceWasPressed)
        {
            if (MeteorLaunch == null)
            {
                MeteorLaunch = StartCoroutine(MeteorTimer());
            }
        }
    }

    private void GameStartCheck()
    {
        if (playerInstance.spaceWasPressed)
        {
            print("space was pressed");
            if (MeteorLaunch == null)
            {
                MeteorLaunch = StartCoroutine(MeteorTimer());
                playerInstance.gameIsRunning = true;
                print("coroutine should have started");
            }
        }
    }
    public IEnumerator MeteorTimer()
    {
        print("meteortimer() has been reached");
        playerInstance.gameIsRunning = true;
        SpawnMeteors();
        waitTime = Random.Range(1f, 10f);
        yield return new WaitForSeconds(waitTime);
        MeteorLaunch = null;
    }

    private void SpawnMeteors()
    {
        if (playerInstance.gameIsRunning)
        {
            GameObject meteor = meteors[Random.Range(0, meteors.Length)];

            GameObject temp = Instantiate(meteor);
            temp.GetComponent<Rigidbody2D>().velocity = GenerateRandomVector();
        }
    }
    private Vector2 GenerateRandomVector()
    {
        randomVecX = GenerateRandomVelocity();
        randomVecY = GenerateRandomVelocity();
        randomVec = new Vector2(randomVecX, randomVecY);
        return randomVec;
    }

    private float GenerateRandomVelocity()
    {
        float num = Random.Range(0, 5);
        float sign = Random.Range(0, 2);

        //generates a float for the velocity
        #region if statements for number
        if (num == 0)
        {
            num = Random.Range(5f, 6f);
        }
        else if (num == 1)
        {
            num = Random.Range(6f, 7f);
        }
        else if (num == 2)
        {
            num = Random.Range(7f, 8f);
        }
        else if (num == 3)
        {
            num = Random.Range(8f, 9f);
        }
        else if (num == 4)
        {
            num = Random.Range(9f, 10f);
        }
        #endregion

        //determines whether velocity is positive or negative
        #region if statements for sign
        //sign = 0 indicates a positive velocity
        if (sign == 0)
        {
            num *= 1;
        }
        //sign = 1 indicates a negative velocity
        if (sign == 1)
        {
            num *= -1;
        }
        #endregion
        return num;
    }

        /*
        //array of objects that will be spawned
        [SerializeField] private GameObject[] meteors;

        //boundaries of the game area
        private float xMin;
        private float yMin;
        private float xMax;
        private float yMax;

        public PlayerController playerInstance;

        public Coroutine MeteorLaunch;
        private float waitTime;

        void Start()
        {
            xMin = Globals.GameBounds.min.x;
            yMin = Globals.GameBounds.min.y;
            xMax = Globals.GameBounds.extents.x;
            yMax = Globals.GameBounds.extents.y;
        }

        // Update is called once per frame
        void Update()
        {
            if(!playerInstance.gameIsRunning)
            {
                print("gamestartcheck should run");
                GameStartCheck();
            }
            else if(playerInstance.gameIsRunning && playerInstance.spaceWasPressed)
            {

            }
        }

        private void GameStartCheck()
        {
            print("gamestartcheck did run");
            if(playerInstance.spaceWasPressed)
            {
                print("space was pressed");
                if (MeteorLaunch == null)
                {
                    MeteorLaunch = StartCoroutine(MeteorTimer());
                    print("coroutine should have started");
                }
            }
        }

        public IEnumerator MeteorTimer()
        {
            print("meteortimer() has been reached");
            playerInstance.gameIsRunning = true;
            SpawnMeteors();
            waitTime = Random.Range(1f, 10f);
            yield return new WaitForSeconds(waitTime);
            MeteorLaunch = null;
        }

        private void SpawnMeteors()
        {
            if(playerInstance.gameIsRunning)
            {
                print("meteor should have been launched");
                Vector2 pos = new Vector2(Random.Range(xMin,xMax), Random.Range(yMin,yMax));

                GameObject meteorsPrefab = meteors[Random.Range(0, meteors.Length)];

                Instantiate(meteorsPrefab, pos, transform.rotation);


            }
        }
        */
    }
