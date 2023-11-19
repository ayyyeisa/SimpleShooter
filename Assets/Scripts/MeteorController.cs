using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    [SerializeField] float size = 1f;

    [SerializeField] float minSize = 0.5f;

    [SerializeField] float maxSize = 1.5f;

    private SpriteRenderer meteorSprite;
    private Rigidbody2D meteor;

    // Start is called before the first frame update
    void Start()
    {
        meteorSprite = GetComponent<SpriteRenderer>();
        meteor = GetComponent<Rigidbody2D>();

        meteorSprite.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360f);
        this.transform.localScale = Vector3.one * this.size;

        meteor.mass = this.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
