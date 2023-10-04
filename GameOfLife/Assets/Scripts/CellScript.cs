using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool nextAlive;
    public bool alive;
    public int aliveNeigbors;
    public List <CellScript> neighborCells = new List<CellScript>();
    private SpriteRenderer sprite;
    private float alphafloat;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        alive = nextAlive;
        spriteRenderer.enabled = alive;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        neighborCells.Add(collision.GetComponent<CellScript>());
    }
    public void ResetNeighborCells()
    {
        aliveNeigbors = 0;
    }
    public void UpdateNeighbors()
    {
        foreach (CellScript cell in neighborCells)
        {
            if (cell.nextAlive)
            {
                aliveNeigbors++;
            }
        }
    }
    public void spriteState()
    {
        if (!alive && !nextAlive) 
        {
            alphafloat = 0.1f;
            sprite.color = new Color(1, 1, 1, alphafloat);
        }
    }
}
