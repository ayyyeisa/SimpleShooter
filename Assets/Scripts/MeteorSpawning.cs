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
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{
    [SerializeField] PlayerController playerInstance;
    [SerializeField] MeteorController meteorPrefab;

    public Coroutine MeteorRef;


    [SerializeField] float spawnRate = 3f;
    [SerializeField] float spawnDistance = 11f;
    [SerializeField] int spawnAmount = 1;
    [SerializeField] float trajectoryVariance = 15f;


    private void Update()
    {
        if(playerInstance.gameIsRunning)
        {   
            //spawns a meteor object every three seconds
            //InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);

            if(MeteorRef == null)
            {
                MeteorRef = StartCoroutine(StartMeteorSpawns());
            }

        }
    }

    public IEnumerator StartMeteorSpawns()
    {
        yield return new WaitForSeconds(spawnRate);
        Spawn();
        MeteorRef = null;
    }

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
