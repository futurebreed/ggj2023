using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    // This script will generate an NxM grid of Cube prefabs
    // using the X and Z axis as the Y anx X coordinates for the grid system respectively

    // The prefab to be used for the grid
    public GridCell cubeCellPrefab, sphereCellPrefab;

    // The size of the grid
    public int gridWidth = 32;
    public int gridHeight = 16;

    // TODO: turn this into an enum? idfk
    private char[,] _tileMap;
    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Awake()
    {
        // Grab the main camera's transform so we can
        // position it relative to the grid
        _cameraTransform = Camera.main.transform;

        // lets parse the level data from the resources directory
        // and generate the grid based on that data
        TextAsset levelText = Resources.Load<TextAsset>("Levels/ShrimpleLevel");

        // turn the text into a multidimensional array

        // Generate the grid
        GenerateGrid();
    }

    // Generate the grid
    void GenerateGrid()
    {
        int i = 0;
        // Loop through the grid width
        for (int gridX = 0; gridX < gridWidth; gridX++)
        {
            // Loop through the grid height
            for (int gridY = 0; gridY < gridHeight; gridY++)
            {
                // TODO: Sample the Level's tilemap to see if there is a cell at this position
                // And if so, what type of cell it is

                GridCell prefabToSpawn = i % 3 == 0 ? sphereCellPrefab : cubeCellPrefab;
                i++; // hack

                // Create a new grid prefab
                GridCell newGridPrefab = Instantiate(prefabToSpawn);

                // In root grid, z-axis == y-axis
                // Set the position of the new grid prefab
                newGridPrefab.transform.position = new Vector3(gridX, 0, gridY);

                // Set the parent of the new grid prefab
                newGridPrefab.transform.parent = this.transform;
            }
        }

        // Position the camera in the center of the grid
        _cameraTransform.transform.position = new Vector3((float)gridWidth / 2, _cameraTransform.position.y, (float)gridHeight / 2);
    }

}
