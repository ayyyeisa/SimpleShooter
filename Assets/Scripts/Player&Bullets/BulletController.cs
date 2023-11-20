/*****************************************************************************
// File Name : BulletController.cs
// Author : Isa Luluquisin
// Creation Date : November 20, 2023
//
// Brief Description : This controls the behavior bullet gameobjects.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float speed = 500f;
    [SerializeField] private float maxLifetime = 10f;

    private void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        bullet.AddForce(direction * speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Meteor")
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
