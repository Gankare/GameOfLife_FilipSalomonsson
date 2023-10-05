using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellScript : MonoBehaviour
{
    public bool nextAlive;
    public bool alive;
    public int aliveNeigbors;
    public List <CellScript> neighborCells = new List<CellScript>();
    private float alphafloat;
    private SpriteRenderer spriteRenderer;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();

        if (alive && !nextAlive)
        {
            alphafloat = 0.75f;
            spriteRenderer.color = new Color(0.17f, 0.17f, 0.45f, alphafloat);
        }
        else if (!alive && !nextAlive)
        {
            float decreasealpha = 0.25f;
            alphafloat -= decreasealpha;
            spriteRenderer.color = new Color(0.17f, 0.17f, 0.45f, alphafloat);
        }
        else 
        {
            alphafloat = 1;
            spriteRenderer.color = new Color(1, 1, 1, alphafloat);
        }

        alive = nextAlive;
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
}
