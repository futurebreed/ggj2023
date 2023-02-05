using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // The prefabs to be used for the grid
    public GridCell emptyCellPrefab, cubeCellPrefab, sphereCellPrefab, dirtPrefab, rockPrefab, exitCell;

    // The size of the grid
    public int gridWidth = 32;
    public int gridHeight = 16;

    private Transform _cameraTransform;
    private Grid _grid;
    public Grid Grid => _grid;

    // Start is called before the first frame update
    void Awake()
    {
        // NOTE: if we want to make variable grid sizes this will need to be changed
        char[,] tileMap = new char[gridWidth, gridHeight];
        _grid = new Grid(gridWidth, gridHeight);

        // Grab the main camera's transform so we can
        // position it relative to the grid
        _cameraTransform = Camera.main.transform;

        // Pull the current level from the scene navigation controller singleton
        int currentStage = SceneNavigationController.ActiveStage;

        // The file level format is a 32x16 comma separated list of characters
        // representing the tilemap
        string[] rows = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, $"Levels//Level{currentStage}.txt"));

        // The coordinates in the file are read from Y=height to Y=0 and X=0 to X=width
        // The grid is rendered from Y=0 to Y=height and X=0 to X=width
        // So we need to reverse the Y axis
        for (int i = 0; i < rows.Length; i++)
        {
            // Split the row into columns
            string[] columns = rows[i].Split(',');

            // Loop through the columns
            for (int j = 0; j < columns.Length; j++)
            {
                // Set the tilemap data
                tileMap[j, gridHeight - i - 1] = columns[j][0];
            }
        }

        // Generate the grid
        GenerateGrid(tileMap);
    }

    // Generate the grid
    void GenerateGrid(char[,] tileMap) 
    {
        // Loop through the grid width
        for (int gridX = 0; gridX < gridWidth; gridX++)
        {
            // Loop through the grid height
            for (int gridY = 0; gridY < gridHeight; gridY++)
            {
                // Sample the cell-type from the level's tile map
                char cellType = tileMap[gridX, gridY];

                CreateGridCellFromTile(gridX, gridY, cellType);
            }
        }

        // Position the camera in the center of the grid
        _cameraTransform.transform.position = new Vector3((float)gridWidth / 2, (float)gridHeight / 2, _cameraTransform.position.z);
    }

    private void CreateGridCellFromTile(int gridX, int gridY, char cellType)
    {
        GridCell prefabToSpawn = emptyCellPrefab;

        switch (cellType)
        {
            case 'C':
                prefabToSpawn = cubeCellPrefab;
                break;
            case 'S':
                prefabToSpawn = sphereCellPrefab;
                break;
            case 'E':
                prefabToSpawn = emptyCellPrefab;
                break;
            case 'D':
                prefabToSpawn = dirtPrefab;
                break;
            case 'R':
                prefabToSpawn = rockPrefab;
                break;
            case 'X':
                prefabToSpawn = exitCell;
                break;
        }

        // Create a new grid prefab
        GridCell newGridPrefab = Instantiate(prefabToSpawn);
        newGridPrefab.name = $"{newGridPrefab.GetType().Name}({gridX},{gridY})";

        // this probably should be a ctor thing but lolmonobehaviour
        newGridPrefab.SetGridDimensions(gridX, gridY, gridWidth, gridHeight);

        // In root grid, z-axis == y-axis
        // Set the position of the new grid prefab
        newGridPrefab.transform.position = new Vector3(gridX, gridY, 0);

        // Set the parent of the new grid prefab
        newGridPrefab.transform.parent = this.transform;

        // Set the grid cell's position in the grid
        _grid.SetCell(gridX, gridY, newGridPrefab);
    }

}
