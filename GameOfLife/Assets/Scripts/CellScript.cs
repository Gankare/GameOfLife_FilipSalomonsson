using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool alive;

    SpriteRenderer spriteRenderer;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();

        //if (spriteRenderer == null )
        //	spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = alive;
    }
}
