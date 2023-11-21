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
    [Tooltip("Rigidbody of the bullet object")]
    [SerializeField] private Rigidbody2D bullet;
    [Tooltip("speed at which bullet travels")]
    [SerializeField] private float speed = 500f;
    [Tooltip("maximum number of seconds the bullet can be on-screen")]
    [SerializeField] private float maxLifetime = 10f;

    private void Start()
    {
        //sets bullet variable 
        bullet = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Shoots bullet out in a forward direction at the given speed.
    /// Bullet should be destroyed once it has reached the maximum lifetime.
    /// </summary>
    /// <param name="direction">vector2 in which the bullet should travel. should be same direction as player object</param>
    public void Project(Vector2 direction)
    {
        bullet.AddForce(direction * speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    /// <summary>
    /// Handles collisions between bullet and other game objects.
    /// The bullet should be destroyed if it collides with a meteor or the game's boundaries
    /// </summary>
    /// <param name="collision">game object that bullet has collided with</param>
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
