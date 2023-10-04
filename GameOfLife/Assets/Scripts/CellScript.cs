using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool nextAlive;
    public bool alive;
    public int aliveNeigbors;
    public List <CellScript> neighborCells = new List<CellScript>();

    SpriteRenderer spriteRenderer;

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
    public void OnTriggerExit2D(Collider2D collision)
    {
        neighborCells.Remove(collision.GetComponent<CellScript>());
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
}
