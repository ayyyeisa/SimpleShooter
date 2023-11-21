/*****************************************************************************
// File Name : MeteorSpawning.cs
// Author : Isa Luluquisin
// Creation Date : November 19, 2023
//
// Brief Description : This controls the behavior of the meteor spawn point.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{
    [Header("Instances of Objects")]
    [SerializeField] PlayerController playerInstance;
    [SerializeField] MeteorController meteorPrefab;

    //coroutine that controls the spawning of meteors
    public Coroutine MeteorRef;

    [Header("Spawning Variables")]
    [Tooltip("How many seconds there are between the spawning of meteors")]
    [SerializeField] float spawnRate = 3f;
    [Tooltip("How far away meteors will spawn away from the spawning points")]
    [SerializeField] float spawnDistance = 11f;
    [Tooltip("How many meteors are spawned at one time/call")]
    [SerializeField] int spawnAmount = 1;
    [Tooltip("Degrees of variance in the trajectory of new meteors")]
    [SerializeField] float trajectoryVariance = 15f;


    private void Update()
    {
        if(playerInstance.gameIsRunning)
        {  
            if(MeteorRef == null)
            {
                //call on meteor-spawning function if game is running
                MeteorRef = StartCoroutine(StartMeteorSpawns());
            }

        }
        if((!playerInstance.gameIsRunning) && (playerInstance.spaceWasPressed))
        {
            //stops spawning new meteors when game ends
            StopCoroutine(StartMeteorSpawns());
        }
    }

    /// <summary>
    /// Coroutine for meteor spawning. Waits for however many seconds the spawnRate is set to
    /// before calling on spawn function. 
    /// </summary>
    /// <returns>returns a countdown for between the spawning</returns>
    public IEnumerator StartMeteorSpawns()
    {
        yield return new WaitForSeconds(spawnRate);
        Spawn();
        MeteorRef = null;
    }

    /// <summary>
    /// Spawns a meteor a certain distance away from the spawner. The direction and spawnpoint of that 
    /// instance of the meteor are randomized to the extent of the set minsize, maxsize, variance, etc.
    /// </summary>
    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            //random point within the circle
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            //chooses a spawn point based on where the spawner object is placed in unity editor
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            MeteorController meteor = Instantiate(meteorPrefab, spawnPoint, rotation);
            Instantiate(meteor, spawnPoint, rotation);
            meteor.size = Random.Range(meteor.minSize, meteor.maxSize);
            meteor.SetTrajectory(rotation * -spawnDirection);
        }
    }

}
