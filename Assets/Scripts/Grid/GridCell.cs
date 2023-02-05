using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridCell : MonoBehaviour
{
    // the x,y coordinate of the current cell
    protected int _gridX, _gridY;

    // the overall width and height of the grid
    protected int _gridHeight, _gridWidth;

    public void SetGridDimensions(int gridX, int gridY, int gridWidth, int gridHeight)
    {
        this._gridX = gridX;
        this._gridY = gridY;
        this._gridWidth = gridWidth;
        this._gridHeight = gridHeight;
    }

}
