/*****************************************************************************
// File Name : MeteorController.cs
// Author : Isa Luluquisin
// Creation Date : November 19, 2023
//
// Brief Description : This controls the behavior of any meteor gameobject.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [Tooltip("Array of meteor sprites that will be used")]
    [SerializeField] Sprite[] sprites;
    //sprite renderer of meteor object
    private SpriteRenderer meteorSprite;
    [Tooltip("Refers to the meteor prefab with rigidbody2d")]
    public Rigidbody2D meteor;

    [Header("Variables controlling the meteor game object")]
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    [Tooltip("Speed at which meteor is moving")]
    [SerializeField] float speed = 30f;
    [Tooltip("Max number of seconds that a meteor instance will remain on-screen")]
    [SerializeField] float maxLifetime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //sets variables
        meteorSprite = GetComponent<SpriteRenderer>();
        meteor = GetComponent<Rigidbody2D>();

        //randomly chooses a sprite to assign the meteor object based on the array of sprites provided
        meteorSprite.sprite = sprites[Random.Range(0, sprites.Length - 1)];

        //creates a random rotation for the meteor object
        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360f);
        //randomizes the size of the meteor object
        this.transform.localScale = Vector3.one * this.size;

        //changes the mass of a meteor based on the size of the object
        meteor.mass = this.size;
    }

    /// <summary>
    /// Creates a line of trajectory for the meteor to travel towards by adding a force.'
    /// Force is found by multiplying a predetermined direction with the meteor's speed.
    /// Meteor object should then be destroyed if it reaches the maxlifetime.
    /// </summary>
    /// <param name="direction">Vector2 of the direction the meteor should move towards</param>
    public void SetTrajectory(Vector2 direction)
    {
        this.meteor.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    /// <summary>
    /// Handles different collisions between a meteor object and other objects
    /// </summary>
    /// <param name="collision">objects that the meteor has collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destroys the meteor if it hits the player
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
        //destroys and possibly splits the meteor if it is hit by a bullet
        //score is also updated
        else if(collision.transform.tag == "Bullet")
        {
            GameManager GM = FindObjectOfType<GameManager>();
            GM.UpdateScore(this);
            //meteory is split in half if it is bigger than the minimum size
            if((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            //original meteor object is destroyed
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates another meteor object that is half the size of the the previous meteor. The position is slightly randomized
    /// so that no two split meteors spawn in the same place. Each new split object also has a random trajectory.
    /// </summary>
    private void CreateSplit()
    {
        Vector2 positionOffset = this.transform.position;
        positionOffset += Random.insideUnitCircle * 0.5f;

        MeteorController half = Instantiate(this, positionOffset, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }
}
