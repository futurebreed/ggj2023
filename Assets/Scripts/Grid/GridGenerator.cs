using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    // This script will generate an NxM grid of Cube prefabs
    // using the X and Z axis as the Y anx X coordinates for the grid system respectively

    // The prefab to be used for the grid
    public GridCell cubeCellPrefab;

    // The size of the grid
    public int gridX = 10;
    public int gridY = 10;

    // todo: this will be a collection of tile scriptable objects
    private Dictionary<Vector2, GridCell> gridState = new Dictionary<Vector2, GridCell>();

    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Awake()
    {
        // Grab the main camera's transform so we can
        // position it relative to the grid
        _cameraTransform = Camera.main.transform;
        
        // Generate the grid
        GenerateGrid();
    }

    // Generate the grid
    void GenerateGrid()
    {
        // Loop through the grid width
        for (int x = 0; x < gridX; x++)
        {
            // Loop through the grid height
            for (int z = 0; z < gridY; z++)
            {
                // TODO: Sample the Level's tilemap to see if there is a cell at this position
                // And if so, what type of cell it is

                // Create a new grid prefab
                GridCell newGridPrefab = Instantiate(cubeCellPrefab);

                // In root grid, z-axis == y-axis
                // Set the position of the new grid prefab
                newGridPrefab.transform.position = new Vector3(x, 0, z);

                // track the grid state in a simple lookup table
                gridState[new Vector2(x, z)] = newGridPrefab;

                // Set the parent of the new grid prefab
                newGridPrefab.transform.parent = this.transform;
            }
        }

        // Position the camera in the center of the grid
        _cameraTransform.transform.position = new Vector3((float)gridX / 2, _cameraTransform.position.y, (float)gridY / 2);
    }

}
