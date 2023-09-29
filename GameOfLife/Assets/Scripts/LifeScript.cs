using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public GameObject cellPrefab;
    private CellScript[,] cells;
    public List<Transform> aliveCellsList = new List<Transform>();
    private float cellSize = 0.25f; //Size of our cells
    private int numberOfColums, numberOfRows;
    private int spawnChancePercentage = 15;
    public float updateTimer;

    void Start()
    {
        //Lower framerate makes it easier to test and see whats happening.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        //Initiate our matrix array
        cells = new CellScript[numberOfColums, numberOfRows];

        //Create all objects

        //For each row
        for (int y = 0; y < numberOfRows; y++)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; x++)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *
                    Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<CellScript>();

                //Random check to see if it should be alive
                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].alive = true;
                }

                cells[x, y].UpdateStatus();
                if (cells[x, y].alive)
                {
                    aliveCellsList.Add(cells[x, y].GetComponent<Transform>());
                }
            }
        }
    }

    void Update()
    {
        //TODO: Calculate next generation
        updateTimer += Time.deltaTime;

        if (updateTimer >= 0.05)
        {
            UpdateCells();
            updateTimer = 0;
        }

        //TODO: update buffer
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                cells[x, y].UpdateStatus();
            }
        }
    }

    private void UpdateCells()
    {
        foreach (Transform aliveCell in aliveCellsList)
        {

        }
    }
}
