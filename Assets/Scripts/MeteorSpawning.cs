using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{
    [SerializeField] MeteorController meteorPrefab;

    [SerializeField] float spawnRate = 2f;
    [SerializeField] float spawnDistance = 11f;
    [SerializeField] int spawnAmount = 1;
    private void Start()
    {
        //gets called every 2 sec
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint;

            MeteorController meteorPrefab = Instantiate(this.meteorPrefab, spawnPoint, rotation);
        }
    }
}
