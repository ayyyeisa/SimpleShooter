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
    [SerializeField] Sprite[] sprites;

    public float size = 1f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    [SerializeField] float speed = 30f;

    [SerializeField] float maxLifetime = 20f;

    private SpriteRenderer meteorSprite;
    public Rigidbody2D meteor;

    // Start is called before the first frame update
    void Start()
    {
        meteorSprite = GetComponent<SpriteRenderer>();
        meteor = GetComponent<Rigidbody2D>();

        meteorSprite.sprite = sprites[Random.Range(0, sprites.Length - 1)];

        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360f);
        this.transform.localScale = Vector3.one * this.size;

        meteor.mass = this.size;
    }
    public void SetTrajectory(Vector2 direction)
    {
        this.meteor.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Bullet")
        {
            GameManager GM = FindObjectOfType<GameManager>();
            GM.UpdateScore(this);
            if((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 positionOffset = this.transform.position;
        positionOffset += Random.insideUnitCircle * 0.5f;

        MeteorController half = Instantiate(this, positionOffset, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }
}