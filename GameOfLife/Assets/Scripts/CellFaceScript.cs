using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFaceScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Sprite> sprites = new List<Sprite>();
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber;
        spriteNumber = Random.Range(0, 3);
        spriteRenderer.sprite = sprites[spriteNumber];

        

    }
}
