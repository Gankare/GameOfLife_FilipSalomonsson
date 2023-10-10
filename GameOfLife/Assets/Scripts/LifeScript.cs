using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public GameObject cellPrefab;
    private CellScript[,] cells;

    public List<CellScript> aliveCellsScript = new List<CellScript>();
    public List<CellScript> allCells = new List<CellScript>();

    private float cellSize = 0.25f; //Size of our cells
    private int numberOfColums, numberOfRows;
    private int spawnChancePercentage = 20;
    private float updateTimer;

    //To see if simulation becomes stable
    private int stableCells;
    private int stableCellsInRow;
    private int lastStableCells;

    public static int generations;
    public static bool simStable;

    private bool pauseSim = false;

    void Start()
    {
        //For less lag
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 15;

        simStable = false;
        generations = 0;
        //From menu script
        spawnChancePercentage = MenuScript.spawnPrecentage;
        Camera.main.orthographicSize = MenuScript.cameraSize;

        //Lower framerate makes it easier to test and see whats happening.
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);
        cells = new CellScript[numberOfColums, numberOfRows];
        //Initiate our matrix array

        //Create all objects

        //For each row
        for (int y = 0; y < numberOfRows; y++)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; x++)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *Camera.main.aspect + cellSize / 1.5f,
                y * cellSize - Camera.main.orthographicSize + cellSize / 1.5f);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<CellScript>();

                //Random check to see if it should be alive
                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].nextAlive = true;
                }
            }
        }
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                allCells.Add(cells[x, y]);
                cells[x, y].UpdateNeighbors();
                cells[x, y].UpdateStatus();
                if (cells[x, y].nextAlive)
                {
                    aliveCellsScript.Add(cells[x, y]);
                }
            }
        }
    }
    void Update()
    {
        updateTimer += Time.deltaTime;
        if (!pauseSim)
        {
            //Loop
            if (updateTimer >= 0.05f && !simStable)
            {
                StatusUpdate();
                Reset();
                NewAliveCells();
                CheckStableCells();
                NextGenCells();
                updateTimer = 0;
                generations++;
            }
        }
        //pause button
        if (Input.GetKeyDown(KeyCode.Space) && !pauseSim) 
            pauseSim = true;
        else if (Input.GetKeyDown(KeyCode.Space) && pauseSim)
            pauseSim = false;
        //Reset button
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach(CellScript cell in allCells)
            {
                cell.nextAlive = false;
                cell.UpdateStatus();
                stableCellsInRow = 0;
                generations = 0;
            }
        }
    }
    private void StatusUpdate()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                cells[x, y].UpdateStatus();
            }
        }
    }
    private void Reset()
    {
        foreach (CellScript cell in allCells)
        {
            cell.ResetNeighborCells();
        }
        aliveCellsScript.Clear();
        stableCells = 0;
    }
    private void NewAliveCells()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                cells[x, y].UpdateNeighbors();
                if (cells[x, y].nextAlive)
                {
                    stableCells++;
                    aliveCellsScript.Add(cells[x, y]);
                }
            }
        }
    }
    public void CheckStableCells()
    {
        if (stableCells == lastStableCells && stableCells != 0)
        {
            stableCellsInRow++;
        }
        else
            stableCellsInRow = 0;

        if(stableCellsInRow > 15)
        {
            generations -= 15;
            simStable = true;
        }
        lastStableCells = stableCells;
    }
    public void NextGenCells()
    {
        foreach (CellScript cell in aliveCellsScript)
        {
            if (cell.aliveNeigbors == 0)
                cell.nextAlive = false;
            else
                CheckNeighborCell(cell);
        }
    }
    public void CheckNeighborCell(CellScript cell)
    {
        foreach (CellScript neigborCell in cell.neighborCells)
        {
            if (neigborCell.nextAlive && (neigborCell.aliveNeigbors < 2 || neigborCell.aliveNeigbors > 3))
            {
                neigborCell.nextAlive = false;
            }
            else if (!neigborCell.nextAlive && neigborCell.aliveNeigbors == 3)
            {
                neigborCell.nextAlive = true;
            }
            else
            {
                neigborCell.nextAlive = neigborCell.nextAlive;
            }
        }
    }
}
